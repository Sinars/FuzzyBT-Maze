using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

public class SettingsLoader : MonoBehaviour {

    public AudioMixer audioMixer;
    private string path;
	// Use this for initialization
	void Start () {
        path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        LoadData();
    }

    private void LoadData() {
        path += "\\Maze";

        if (!System.IO.Directory.Exists(path)) {
            System.IO.Directory.CreateDirectory(path);
        }
        path += "\\Settings.ini";
        if (File.Exists(path)) {
            string[] data = File.ReadAllText(path).Split(' ');
            if (data.Length > 1) {
                QualitySettings.SetQualityLevel(int.Parse(data[0]));
                if (!audioMixer.SetFloat("masterVolume", float.Parse(data[1]))) {
                    //Debug.Log("unable to set volume");
                }
            }
            else
                WriteSettings();
        }
        else {
            var file = File.Create(path);
            file.Close();
            WriteSettings();
        }
            
    }

    public void SaveSettings() {
        WriteSettings();    
    }
    private void WriteSettings() {
        float volume;
        audioMixer.GetFloat("masterVolume", out volume);
        File.WriteAllText(path, QualitySettings.GetQualityLevel().ToString() + " " + volume.ToString());
    }
	// Update is called once per frame
	void Update () {
		
	}
}
