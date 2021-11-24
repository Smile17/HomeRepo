package com.example.shop;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Toast;

public class MainActivity extends AppCompatActivity {
    public static final String EXTRA_MESSAGE = "shop.extra.MESSAGE";
    private String mOrderMessage;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
    }
    public void displayToast(String message) {
        mOrderMessage = message;
        Toast.makeText(getApplicationContext(), message,
                Toast.LENGTH_SHORT).show();
    }
    public void showWhiteShirtOrder(View view) {
        displayToast(getString(R.string.white_shirt_message));
    }
    public void showBlueShirtOrder(View view) {
        displayToast(getString(R.string.blue_shirt_message));
    }
    public void showPinkShirtOrder(View view) {
        displayToast(getString(R.string.pink_shirt_message));
    }
    public void onClick(View view) {
        Intent intent =
                new Intent(MainActivity.this, OrderActivity.class);
        intent.putExtra(EXTRA_MESSAGE, mOrderMessage);
        startActivity(intent);
    }
}