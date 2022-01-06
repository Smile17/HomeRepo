package com.example.myapplication;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

import com.example.myapplication.ui.login.LoginActivity;

public class ResultsActivity extends AppCompatActivity {
    String userEmail;
    String message;
    private TextView tvMessage;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_results);

        Intent intent = getIntent();
        userEmail = intent.getStringExtra(MainActivity.EXTRA_USER);
        message = intent.getStringExtra(MainActivity.EXTRA_MESSAGE);
        tvMessage = (TextView) findViewById(R.id.tvMessage);
        tvMessage.setText(message);
        Toast.makeText(getApplicationContext(), message, Toast.LENGTH_LONG).show();
    }

    protected void sendEmail(String email, String message) {
        Log.i("Send email", "");
        String[] TO = {email};
        Intent emailIntent = new Intent(Intent.ACTION_SEND);

        emailIntent.setData(Uri.parse("mailto:"));
        emailIntent.setType("text/plain");
        emailIntent.putExtra(Intent.EXTRA_EMAIL, TO);
        emailIntent.putExtra(Intent.EXTRA_SUBJECT, "Game Results");
        emailIntent.putExtra(Intent.EXTRA_TEXT, message);

        try {
            startActivity(Intent.createChooser(emailIntent, "Send mail..."));
            finish();
            Log.i("Finished sending email", "");
        } catch (android.content.ActivityNotFoundException ex) {
            Toast.makeText(this, "There is no email client installed.", Toast.LENGTH_SHORT).show();
        }
    }

    public void btnSend(View view) {
        sendEmail(userEmail, message);
    }
}