package com.example.l4;

import android.content.SharedPreferences;
import android.graphics.Color;
import android.os.Bundle;
import android.preference.CheckBoxPreference;
import android.preference.PreferenceActivity;
import android.preference.PreferenceManager;

public class SettingsActivity extends PreferenceActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        addPreferencesFromResource(R.xml.preferences);
        //Load_setting();
    }
    private void Load_setting(){
        SharedPreferences sp = PreferenceManager.getDefaultSharedPreferences(this);
        boolean chk_night = sp.getBoolean("NIGHT", false);
        if(chk_night){
            getListView().setBackgroundColor(Color.parseColor("#222222"));
        }
        else{
            getListView().setBackgroundColor(Color.parseColor("#ffffff"));
        }
        CheckBoxPreference chk_night_instant = (CheckBoxPreference)findPreference("NIGHT");
        chk_night_instant.setOnPreferenceChangeListener(new android.preference.Preference.OnPreferenceChangeListener() {
            @Override
            public boolean onPreferenceChange(android.preference.Preference preference, Object newValue) {
                boolean yes = (boolean)newValue;
                if(yes){
                    getListView().setBackgroundColor(Color.parseColor("#222222"));
                }
                else{
                    getListView().setBackgroundColor(Color.parseColor("#ffffff"));
                }
                return false;
            }
        });
    }
    @Override
    protected void onResume(){
        //Load_setting();
        super.onResume();
    }
}