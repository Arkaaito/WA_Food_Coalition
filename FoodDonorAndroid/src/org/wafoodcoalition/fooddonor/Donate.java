package org.wafoodcoalition.fooddonor;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.UUID;
import java.util.regex.Pattern;

import org.apache.http.HttpResponse;
import org.json.JSONException;
import org.json.JSONObject;
import org.json.JSONTokener;
import org.wafoodcoalition.fooddonor.location.FoodLocation;
import org.wafoodcoalition.fooddonor.location.LocationDetection;
import org.wafoodcoalition.fooddonor.location.LocationUpdated;
import org.wafoodcoalition.fooddonor.service.HttpUtil;
import org.wafoodcoalition.fooddonor.R;

import android.accounts.Account;
import android.accounts.AccountManager;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.location.LocationManager;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.telephony.TelephonyManager;
import android.util.Log;
import android.util.Patterns;
import android.view.KeyEvent;
import android.view.Menu;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.View.OnKeyListener;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ProgressBar;

public class Donate extends Activity implements LocationUpdated, OnClickListener, OnKeyListener {
    final int minLocationLength = 5;
    final int minItemLength = 20;

    SharedPreferences settings;

    private EditText firstNameEdit;
    private EditText lastNameEdit;
    private EditText emailEdit;
    private EditText phoneEdit;
    private EditText descriptionEdit;
    private EditText notesEdit;
    private EditText addressEdit;
    private EditText cityEdit;
    private EditText zipEdit;
    private CheckBox optInBox;

    FoodLocation detectedLocation;
    FoodLocation location;
    Button foodBankMapButton;
    Button submitButton;
    ProgressBar progressBar;

    private enum PickupStatus {
        NEW,
        SCHEDULED,
        CLOSED,
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_donate);
        settings = PreferenceManager.getDefaultSharedPreferences(this.getApplicationContext());

        firstNameEdit = (EditText) findViewById(R.id.firstName);
        lastNameEdit = (EditText) findViewById(R.id.lastName);
        emailEdit = (EditText) findViewById(R.id.email);
        phoneEdit = (EditText) findViewById(R.id.phone);
        descriptionEdit = (EditText) findViewById(R.id.description);
        notesEdit = (EditText) findViewById(R.id.notes);
        addressEdit = (EditText) findViewById(R.id.streetAddress);
        cityEdit = (EditText) findViewById(R.id.city);
        zipEdit = (EditText) findViewById(R.id.zip);
        optInBox = (CheckBox) findViewById(R.id.optIn);

        loadFirstNameOnView();
        loadLastNameOnView();
        loadEmailOnView();
        loadPhoneOnView();
        loadAddressOnView();
        loadCityOnView();
        loadZipOnView();

        if (settings.getString("deviceId", null) == null) {
            String deviceId = UUID.randomUUID().toString();
            rememberString("deviceId", deviceId);
        }

        //locationEdit.setOnKeyListener(this);

        foodBankMapButton = (Button) findViewById(R.id.foodBankMapButton);
        foodBankMapButton.setOnClickListener(this);

        submitButton = (Button) findViewById(R.id.submit);
        submitButton.setOnClickListener(this);

        progressBar = (ProgressBar) findViewById(R.id.progress);
        progressBar.setVisibility(View.INVISIBLE);
    }

    @Override
    protected void onResume() {
        super.onResume();
        LocationManager lm = (LocationManager)getSystemService(Context.LOCATION_SERVICE);
        LocationDetection.init(this.getApplicationContext());
        LocationDetection.instance().detectLocation(lm, this);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.activity_donate, menu);
        return true;
    }

    public void loadFirstNameOnView() {
        firstNameEdit.setText(settings.getString("firstName", ""));
    }

    public void loadLastNameOnView() {
        lastNameEdit.setText(settings.getString("lastName", ""));
    }

    private String defaultEmailOnView() {
        Pattern emailPattern = Patterns.EMAIL_ADDRESS; // API level 8+
        Account[] accounts = AccountManager.get(this.getApplicationContext()).getAccounts();

        for (Account account : accounts) {
            if (emailPattern.matcher(account.name).matches()) {
                return account.name;
            }
        }
        return "";
    }

    public void loadEmailOnView() {
        String defaultEmail = defaultEmailOnView();
        emailEdit.setText(settings.getString("email", defaultEmail));
    }

    private String defaultPhoneOnView() {
        String phoneNumber = "";

        TelephonyManager tMgr = (TelephonyManager)getSystemService(Context.TELEPHONY_SERVICE);
        if (tMgr != null) phoneNumber = tMgr.getLine1Number();

        return phoneNumber;
    }

    public void loadPhoneOnView() {
        String defaultPhone = defaultPhoneOnView();
        phoneEdit.setText(settings.getString("phone", defaultPhone));
    }

    public void loadAddressOnView() {
        addressEdit.setText(settings.getString("streetAddress", ""));
    }

    public void loadCityOnView() {
        cityEdit.setText(settings.getString("city", ""));
    }

    public void loadZipOnView() {
        zipEdit.setText(settings.getString("zip", ""));
    }

    public void locationSet() {
        submitButton.setEnabled(true);
        submitButton.setText("Request food pickup");
        submitButton.postInvalidate();
    }

    public void locationUnset() {
        submitButton.setEnabled(false);
        submitButton.setText("Detecting location ...");
        submitButton.postInvalidate();
    }

    /** location **/
    public void updated(FoodLocation l) {
        if(l!=null && detectedLocation==null) {
            this.detectedLocation = l;
            String typedLocation = addressEdit.getText().toString();
            if (checkLocationText(typedLocation)) {
                addressEdit.setText(l.getAddress());
                addressEdit.postInvalidate();
            }
            locationSet();
        }
    }

    public void failed() {

    }

    /** location **/

    public void onClick(View arg0) {
        if(arg0==submitButton) {
            submit();
        } else if (arg0==foodBankMapButton) {
            showFoodBankMap();
        }
    }

    private void showFoodBankMap() {
        String url = "http://www.wafoodcoalition.org/membership#map";
        Intent i = new Intent(Intent.ACTION_VIEW);
        i.setData(Uri.parse(url));
        startActivity(i);
    }

    private void submit() {
        String resolvedAddress = getLocationText();
        if (checkRequiredFields() == false) {
            return;
        }
        if(resolvedAddress == null) {
            showLocationMissingAlert();
            return;
        }

        rememberString("firstName", firstNameEdit.getText().toString());
        rememberString("lastName", lastNameEdit.getText().toString());
        rememberString("email", emailEdit.getText().toString());
        rememberString("phone", phoneEdit.getText().toString());
        rememberString("streetAddress", resolvedAddress);
        rememberString("city", cityEdit.getText().toString());
        rememberString("zip", zipEdit.getText().toString());
        updateAddress();
        postToService();
    }

    private boolean checkRequiredFields() {
        if (descriptionEdit.getText().toString().length() < minItemLength) {
            showRequiredDescriptionAlert();
            return false;
        }

        if (emailEdit.getText().toString().length() == 0 &&
                phoneEdit.getText().toString().length() == 0) {
            showRequiredContactAlert();
            return false;
        }

        return true;
    }

    private void updateAddress() {
        try {
            String newAddress = addressEdit.getText().toString();
            Log.v("location", "Trying to update address to "+newAddress);
            if(detectedLocation == null || !detectedLocation.getAddress().equals(newAddress)) {
                Log.v("location", "About to start geocode");
                location = LocationDetection.instance().geoCode(newAddress);
            } else {
                Log.v("location", "No location change");
                location = detectedLocation;
            }
        } catch(Exception e) {
            Log.v("location", "Failed to update address with "+e.getMessage());
            showLocationFailure(e);
        }
    }

    private void showLocationFailure(Exception e) {
        new AlertDialog.Builder(this)
        .setTitle("Warning!  We could not find your address on Google Maps.  You may be contacted for directions.")
        .setMessage(e.getMessage())
        .setPositiveButton("OK", new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int which) {
                // continue
            }
        })
        .show();
    }

    private void showRequiredDescriptionAlert() {
        new AlertDialog.Builder(this)
        .setTitle("Donation Description Missing")
        .setMessage("Please enter the types of food, "
                + "the amount of food, and any pickup instructions "
                + "you have for the food that you're donating. (minimum of 20 characters required). ")
                .setPositiveButton("OK", new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int which) {
                        // continue
                    }
                })
                .show();
    }

    private void showRequiredContactAlert() {
        new AlertDialog.Builder(this)
        .setTitle("Required Contact Information Missing")
        .setMessage("Please provide either your email address or your phone number.")
        .setPositiveButton("OK", new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int which) {
                // continue
            }
        })
        .show();
    }

    private void showNetworkAlert() {
        progressBar.setVisibility(View.INVISIBLE);
        new AlertDialog.Builder(this)
        .setTitle("Network Connection Failure")
        .setMessage("Please turn on data network or wi-fi.")
        .setPositiveButton("OK", new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int which) {
                // continue
            }
        })
        .show();
    }

    private void showLocationMissingAlert() {
        new AlertDialog.Builder(this)
        .setTitle("Location not available")
        .setMessage("We could not detect your location. You cannot post a donation.")
        .setPositiveButton("OK", new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int which) {
                // continue
            }
        })
        .show();
    }

    private void showLocationOutOfRangeAlert() {
        new AlertDialog.Builder(this)
        .setTitle("Only for WA state.")
        .setMessage("Your location is outside of Washington state. You cannot post donation food pickup requests using this app.")
        .setPositiveButton("OK", new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int which) {
                // continue
            }
        })
        .show();
    }

    private void showPostedAlert() {
        new AlertDialog.Builder(this)
        .setTitle("Your request was posted.")
        .setMessage("Thank you. Your nearest WA food bank will contact you.")
        .setPositiveButton("Finish", new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int which) {
                Donate.this.finish();
            }
        })
        .show();
    }

    private void showPostFailedAlert(Integer code) {
        new AlertDialog.Builder(this)
        .setTitle("Your request failed.")
        .setMessage("Your donation post failed with error code:"+code+". Make sure your data network is available, and try again.")
        .setPositiveButton("OK", new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int which) {
                // continue
            }
        })
        .show();
    }

    private void rememberString(String key, String value) {
        SharedPreferences.Editor editor = settings.edit();
        editor.putString(key, value);
        editor.commit();
    }

    private boolean checkLocation() {
        if(location==null) {
            showLocationMissingAlert();
            return false;
        }
        if (!(location.getLat()  > 44.5 &&
                location.getLat()  < 49.2 &&
                location.getLng() > -125.43 &&
                location.getLng() < -116.8)) {
            showLocationOutOfRangeAlert();
            return false;
        }
        return true;
    }

    private void postToService() {
        //{"Name":"NameTestX","Email":"some@hotmail.com","Phone":"5555555555","Address":"some random place","Latitude":16.0,"Longitude":65.0,"Description":"5 pounds of potatoes","Status":"New","ExpirationDate":"2013-10-12T12:55:45","FoodBankID":0}]
        try {
            String locationText = getLocationText();
            if (locationText == null) {
                return;
            }
            JSONObject obj = new JSONObject();
            obj.put("Items", descriptionEdit.getText().toString());
            obj.put("Notes", notesEdit.getText().toString());
            obj.put("Address", locationText);
            obj.put("City", cityEdit.getText().toString());
            obj.put("Zip", zipEdit.getText().toString());
            //obj.put("Status", PickupStatus.NEW.ordinal());
            int donorId = settings.getInt("donorId", 0);
            if (donorId == 0) {
                JSONObject donor = new JSONObject();
                donor.put("DeviceId", settings.getString("deviceId", ""));
                donor.put("FirstName", firstNameEdit.getText().toString());
                donor.put("LastName", lastNameEdit.getText().toString());
                donor.put("Email", emailEdit.getText().toString());
                donor.put("Phone", phoneEdit.getText().toString());
                donor.put("OptIn", optInBox.isChecked());
                obj.put("Donor", donor);
            } else {
                obj.put("DonorId", donorId);
            }

            progressBar.bringToFront();
            progressBar.setVisibility(View.VISIBLE);

            // Disable the submit button
            submitButton.setEnabled(false);


            //new DonateTask(obj, "http://10.105.68.46/api/pickups").execute();
            new DonateTask(obj, "http://70.42.168.133/api/pickups").execute();
        } catch (JSONException e) {
            Log.e("JSON", e.getMessage());
        }
    }

    class DonateTask extends AsyncTask<Void, Void, HttpResponse> {
        private String url;
        private JSONObject obj;
        public DonateTask(JSONObject obj, String url) {
            this.url = url;
            this.obj = obj;
        }
        @Override
        protected HttpResponse doInBackground(Void... unused) {
            try {
                return HttpUtil.post(obj, url);
            } catch (IOException e) {
                Log.e(e.getClass().getName(), e.getMessage(), e);
                return null;
            }
        }

        @Override
        protected void onProgressUpdate(Void... unused) {

        }

        @Override
        protected void onPostExecute(HttpResponse response) {
            if (response != null) {
                progressBar.setVisibility(View.INVISIBLE);
                submitButton.setEnabled(true);

                Integer sResponse = response.getStatusLine().getStatusCode();
                if(sResponse>0) {
                    if(sResponse==201) {
                        showPostedAlert();

                        StringBuilder jsonBody = new StringBuilder();
                        String inputLine;
                        BufferedReader in;
                        try {
                            in = new BufferedReader(new InputStreamReader(response.getEntity().getContent()));
                            while ((inputLine = in.readLine()) != null) {
                                jsonBody.append(inputLine).append("\n");
                            }
                            in.close();
                            JSONTokener t = new JSONTokener(jsonBody.toString());
                            JSONObject result = new JSONObject(t);
                            SharedPreferences.Editor editor = settings.edit();
                            editor.putInt("donorId", result.getInt("DonorId"));
                            editor.commit();
                        } catch (IllegalStateException e) {
                            // TODO Auto-generated catch block
                            e.printStackTrace();
                        } catch (IOException e) {
                            // TODO Auto-generated catch block
                            e.printStackTrace();
                        } catch (JSONException e) {
                            // TODO Auto-generated catch block
                            e.printStackTrace();
                        }
                    } else {
                        showPostFailedAlert(sResponse);
                    }
                } else {
                    showNetworkAlert();
                }
            }
        }
    }

    public String getLocationText() {
        String locationText = addressEdit.getText().toString();
        Log.d("location", "User set location is "+locationText);
        if (!checkLocationText(locationText)) {
            if (!checkLocation()) return null;
            else locationText = location.getAddress();
        }
        return locationText;
    }

    public boolean checkLocationText(String loc) {
        if (loc != null && loc.length() >= minLocationLength) {
            return true;
        } else {
            return false;
        }
    }

    public boolean onKey(View v, int keyCode, KeyEvent event) {
        /*
        EditText view = (EditText)v;
        if (checkLocationText(view.getText().toString())) {
            locationSet();
        } else {
            locationUnset();
        }
         */
        return false;
    }

}
