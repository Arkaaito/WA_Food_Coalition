package org.wafoodcoalition.givecamp.fooddonor;

import java.io.IOException;
import java.util.Calendar;
import java.util.regex.Pattern;

import org.json.JSONException;
import org.json.JSONObject;
import org.wafoodcoalition.givecamp.fooddonor.location.FoodLocation;
import org.wafoodcoalition.givecamp.fooddonor.location.LocationDetection;
import org.wafoodcoalition.givecamp.fooddonor.location.LocationUpdated;
import org.wafoodcoalition.givecamp.fooddonor.service.HttpUtil;

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
import android.view.Menu;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ProgressBar;

public class Donate extends Activity implements LocationUpdated, OnClickListener {
	SharedPreferences settings;
	
	private EditText phone;
	private EditText email;
	private DatePicker dpResult;
	private EditText nameEdit;
	private EditText descriptionEdit;
	
	EditText locationEdit = null;
	FoodLocation detectedLocation;
	FoodLocation location;
	Button foodBankMapButton;
	Button submitButton;
	ProgressBar progressBar;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_donate);
		settings = PreferenceManager.getDefaultSharedPreferences(this.getApplicationContext());

		loadNameOnView();
		setDefaultPhoneOnView();
		setCurrentDateOnView();
		setDefaultEmailOnView();
		
		locationEdit = (EditText) findViewById(R.id.location);
		nameEdit = (EditText) findViewById(R.id.name);
		descriptionEdit = (EditText) findViewById(R.id.description);
	    
		foodBankMapButton = (Button) findViewById(R.id.foodBankMapButton);
		foodBankMapButton.setOnClickListener(this);
		
	    submitButton = (Button) findViewById(R.id.submit);
	    submitButton.setEnabled(false);
	    submitButton.setText("Detecting location ...");
	    submitButton.postInvalidate();
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
	
	public void loadNameOnView() {
		nameEdit = (EditText) findViewById(R.id.name);
		nameEdit.setText(settings.getString("name", ""));
	}
	
	// display current date
	public void setCurrentDateOnView() {
	 
		dpResult = (DatePicker) findViewById(R.id.dpResult);
	 
		final Calendar c = Calendar.getInstance();
		int year = c.get(Calendar.YEAR);
		int month = c.get(Calendar.MONTH);
		int day = c.get(Calendar.DAY_OF_MONTH);
	 
		// set current date into date picker
		dpResult.init(year, month, day, null);
	}
	
	public void setDefaultPhoneOnView() {
		
		phone = (EditText) findViewById(R.id.phone);
		
		TelephonyManager tMgr = (TelephonyManager)getSystemService(Context.TELEPHONY_SERVICE);
		String phoneNumber = tMgr.getLine1Number();
		
		if (phoneNumber != null) {
			phone.setText(phoneNumber.substring(1));
		}
	}
	
	public void setDefaultEmailOnView() {
		
		email = (EditText) findViewById(R.id.email);
		
		Pattern emailPattern = Patterns.EMAIL_ADDRESS; // API level 8+
		Account[] accounts = AccountManager.get(this.getApplicationContext()).getAccounts();
		
		for (Account account : accounts) {
			if (emailPattern.matcher(account.name).matches()) {
				email.setText(account.name);
				return;
			}
		}
	}
	
	/** location **/
	public void updated(FoodLocation l) {
		if(l!=null && detectedLocation==null) {
			this.detectedLocation = l;
			String typedlocation = locationEdit.getText().toString();
			if(typedlocation==null || typedlocation.length()<5) {
				locationEdit.setText(l.getAddress());
				locationEdit.postInvalidate();
			}
			submitButton.setEnabled(true);
		    submitButton.setText("Request food pickup");
		    submitButton.postInvalidate();
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
		if (checkRequiredFields() == false) {
			return;
		}
		if(detectedLocation==null) {
			showLocationMissingAlert();
			return;
		}
		rememberString("name", nameEdit.getText().toString());
		updateAddress();
		postToService();
	}
	
	private boolean checkRequiredFields() {
		if (descriptionEdit.getText().toString().length() < 20) {
			showRequiredDescriptionAlert();
			return false;
		}
		
		if (email.getText().toString().length() == 0 &&
					phone.getText().toString().length() == 0) {
			showRequiredContactAlert();
			return false;
		}
		
		return true;
	}
	
	private void updateAddress() {
		try {
			String newAddress = locationEdit.getText().toString();
			if(!detectedLocation.getAddress().equals(newAddress)) {
				location = LocationDetection.instance().geoCode(newAddress);
			} else {
				location = detectedLocation;
			}
		} catch(Exception e) {
			showLocationFailure(e);
		}
	}
	
	private void showLocationFailure(Exception e) {
		new AlertDialog.Builder(this)
	    .setTitle("Address not found on Map.")
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
		progressBar.setVisibility(View.INVISIBLE); //in case its showing.
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
	
	private String getDateStringFromDatePicker() {
		String time = "T12:55:55";
		String dateString = 
				String.valueOf(dpResult.getYear()) + "-" +
				String.format("%02d", 1 + dpResult.getMonth()) + "-" + // month is 0 indexed
				String.format("%02d", dpResult.getDayOfMonth()) +
				time;
		
		return dateString;
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

			if(!checkLocation()) return;
			JSONObject obj = new JSONObject();
			obj.put("Name", nameEdit.getText().toString());
			obj.put("Email", email.getText().toString());
			obj.put("Phone", phone.getText().toString());
			obj.put("Address", location.getAddress());
			obj.put("Latitude", location.getLat());
			obj.put("Longitude", location.getLng());
			obj.put("Description", descriptionEdit.getText());
			obj.put("Status", "Open");
			obj.put("ExpirationDate", getDateStringFromDatePicker()); //"2013-10-14T12:55:55"
			obj.toString();
			
			progressBar.bringToFront();
			progressBar.setVisibility(View.VISIBLE);
			
			// Disable the submit button
			submitButton.setEnabled(false);
			
			new DonateTask(obj, "http://sgcwfcorg00.web803.discountasp.net/api/Donation").execute();
		} catch (JSONException e) {
			Log.e("JSON", e.getMessage());
		}
	}
	
	class DonateTask extends AsyncTask<Void, Void, Integer> {
		private String url;
		private JSONObject obj;
		public DonateTask(JSONObject obj, String url) {
			this.url = url;
			this.obj = obj;
		}
		@Override
		protected Integer doInBackground(Void... unsued) {
			try {	
				return HttpUtil.post(obj, url);
			} catch (IOException e) {
				Log.e(e.getClass().getName(), e.getMessage(), e);
				return 0;
			}
		}

		@Override
		protected void onProgressUpdate(Void... unsued) {

		}

		@Override
		protected void onPostExecute(Integer sResponse) {
			Log.v("POST", String.valueOf(sResponse));
			progressBar.setVisibility(View.INVISIBLE);
			submitButton.setEnabled(true);
			
			if(sResponse>0) {
				if(sResponse==201) {					
					showPostedAlert();
				} else {
					showPostFailedAlert(sResponse);
				}
			} else {
				showNetworkAlert();
			}
		}
	}
}
