package com.example.l4;

import androidx.appcompat.app.AppCompatActivity;
import androidx.core.content.ContextCompat;

import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Color;
import android.os.Bundle;
import android.os.CountDownTimer;
import android.preference.PreferenceManager;
import android.view.View;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

import java.util.concurrent.ThreadLocalRandom;

public class MainActivity extends AppCompatActivity {

    private TextView tvLeft;
    private TextView tvRight;
    private TextView tvTimeLeft;
    String[] ColorNames = {"Красный", "Зеленый", "Синий", "Чорный", "Желтый"};
    int[] СolorValues = {Color.RED, Color.GREEN, Color.BLUE, Color.BLACK, Color.YELLOW};
    int idxLeftColorName;
    int idxRightColorValue;
    int AttemptCount = 0;
    int Count = 0;
    CountDownTimer timer;
    boolean isCounterRunning = false;
    private ProgressBar progressBar;

    // Settings
    int gameTime;
    int gameLevel;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        SetupPreferences();

        tvLeft = (TextView) findViewById(R.id.tvLeft);
        tvRight = (TextView) findViewById(R.id.tvRight);
        tvTimeLeft = (TextView) findViewById(R.id.tvTimeLeft);
        progressBar = (ProgressBar) findViewById(R.id.pbGameProgress);
        SetupGame();

    }
    @Override
    protected void onResume(){
        SetupPreferences();
        SetupGame();
        super.onResume();
    }
    private void SetupPreferences(){
        SharedPreferences sp = PreferenceManager.getDefaultSharedPreferences(this);
        boolean chk_night = sp.getBoolean("NIGHT", false);
        View someView = findViewById(R.id.formMain);
        View root = someView.getRootView();
        TextView tv = (TextView) findViewById(R.id.textView);
        tvTimeLeft = (TextView) findViewById(R.id.tvTimeLeft);
        if(chk_night){
            root.setBackgroundColor(Color.parseColor("#222222"));
            tv.setTextColor(Color.parseColor("#ffffff"));
            tvTimeLeft.setTextColor(Color.parseColor("#ffffff"));
        }
        else{
            root.setBackgroundColor(Color.parseColor("#ffffff"));
            tv.setTextColor(Color.parseColor("#222222"));
            tvTimeLeft.setTextColor(Color.parseColor("#222222"));
        }
        String gameTimeStr = sp.getString("GAMETIME", "20");
        gameTime = Integer.valueOf(gameTimeStr);
        String gameLevelStr = sp.getString("GAMELEVEL", "2");
        gameLevel = Integer.valueOf(gameLevelStr);
    }
    private void SetupGame(){
        GenerateGame();
        if(isCounterRunning){
            isCounterRunning = false;
            timer.cancel();
        }
        progressBar.setMax(gameTime);
        timer = new CountDownTimer(gameTime * 1000, 1000) {

            public void onTick(long millisUntilFinished) {
                int timeLeft = (int)(millisUntilFinished / 1000);
                tvTimeLeft.setText("" + timeLeft);
                progressBar.setProgress(timeLeft);
            }

            public void onFinish() {
                tvTimeLeft.setText("Правильних відповідей: " + Count + " з " + AttemptCount);
                progressBar.setProgress(0);
            }
        };
        StartGame();
    }
    private void StartGame()
    {
        Count = AttemptCount = 0;
        if(!isCounterRunning){
            isCounterRunning = true;
            timer.start();
        }
        else{
            timer.cancel(); // cancel
            timer.start();  // then restart
        }

    }
    private void GenerateGame()
    {
        int max = gameLevel; int min = 0;
        int idx;
        idx = ThreadLocalRandom.current().nextInt(min, max + 1);
        tvLeft.setTextColor(СolorValues[idx]);
        idx = ThreadLocalRandom.current().nextInt(min, max + 1);
        idxLeftColorName = idx;
        tvLeft.setText(ColorNames[idx]);
        idx = ThreadLocalRandom.current().nextInt(min, max + 1);
        idxRightColorValue = idx;
        tvRight.setTextColor(СolorValues[idx]);
        idx = ThreadLocalRandom.current().nextInt(min, max + 1);
        tvRight.setText(ColorNames[idx]);
    }

    public void btnYes(View view) {
        AttemptCount++;
        if(idxLeftColorName == idxRightColorValue)
        {
            Toast toast = Toast.makeText(this, "Чудово",
                    Toast.LENGTH_SHORT);
            toast.show();
            Count++;
        }
        GenerateGame();
    }

    public void btnNo(View view) {
        AttemptCount++;
        if(idxLeftColorName != idxRightColorValue)
        {
            Toast toast = Toast.makeText(this, "Чудово",
                    Toast.LENGTH_SHORT);
            toast.show();
            Count++;
        }
        GenerateGame();
    }

    public void btnAgain(View view) {
        StartGame();
    }

    public void btnSettings(View view) {
        Intent i = new Intent(MainActivity.this, SettingsActivity.class);
        startActivity(i);
    }
}