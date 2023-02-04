using System.Collections.Generic;
using UnityEngine;

public class TextsManager : MonoBehaviour
{
	private const string kTextFileName = "Texts";
	private const string kDialoguesKey = "dialogues";
	private const string kErrorText = "ERROR";
	private Dictionary<string, string> _textsDictionary = new Dictionary<string, string>();

	private static TextsManager _instance = null;
	public static TextsManager Instance => _instance;

	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	private void Start()
	{
		TextAsset texts = Resources.Load(kTextFileName) as TextAsset;
		if (texts != null)
		{
			Dictionary<string, object> json = (Dictionary<string, object>)MiniJSON.Json.Deserialize(texts.text);
			object obj = json.GetValueOrDefault(kDialoguesKey);
			Dictionary<string, object> dialogues = (Dictionary<string, object>)obj;
			foreach (KeyValuePair<string, object> pair in dialogues)
			{
				_textsDictionary.Add(pair.Key, (string)pair.Value);
			}
		}
	}

	public string GetDialogueText(string key)
	{
		return _textsDictionary.ContainsKey(key) ? _textsDictionary[key] : kErrorText;
	}
}
