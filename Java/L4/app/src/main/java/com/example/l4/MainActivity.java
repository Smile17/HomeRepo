package com.example.l4;

import androidx.appcompat.app.AppCompatActivity;

import android.graphics.Color;
import android.os.Bundle;
import android.os.CountDownTimer;
import android.view.View;
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

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        tvLeft = (TextView) findViewById(R.id.tvLeft);
        tvRight = (TextView) findViewById(R.id.tvRight);
        tvTimeLeft = (TextView) findViewById(R.id.tvTimeLeft);
        GenerateGame();
        timer = new CountDownTimer(10000, 1000) {

            public void onTick(long millisUntilFinished) {
                tvTimeLeft.setText("" + millisUntilFinished / 1000);
            }

            public void onFinish() {
                tvTimeLeft.setText("Правильних відповідей: " + Count + " з " + AttemptCount);
            }
        };
        StartGame();

    }
    private void StartGame()
    {
        Count = AttemptCount = 0;
        if( !isCounterRunning ){
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
        int max = 4; int min = 0;
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
}