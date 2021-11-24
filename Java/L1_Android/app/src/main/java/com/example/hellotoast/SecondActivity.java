package com.example.hellotoast;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.widget.TextView;

public class SecondActivity extends AppCompatActivity {
    private TextView mShowCount;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_second);
        Bundle extras = getIntent().getExtras();
        if (extras != null) {
            String mCount_value = extras.getString("mCount");
            mShowCount = (TextView) findViewById(R.id.tv_count);
            if (mShowCount != null)
                mShowCount.setText(mCount_value );
        }
    }
}