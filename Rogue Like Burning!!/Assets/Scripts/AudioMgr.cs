using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//音楽ファイル　  AudioClip
//スピーカー　     AudioSource
//マイク               AudioListener
public class AudioMgr : SingletonMonoBehaviour<AudioMgr> {

    /// SEチャンネル数
    const int SE_CHANNEL = 4;

    /// サウンド種別
    enum eType
    {
        Bgm, // BGM
        Se,  // SE
    }
    // オーディオソース
  AudioSource mBGMSource = null; // BGM
  AudioSource mSESource = null; // SE (デフォルト)
  AudioSource[] mSESourceArray; // SE (チャンネル)

  // BGMにアクセスするためのテーブル
  Dictionary<string, AudioClip> mBGMPool = new Dictionary<string, AudioClip>();
  // SEにアクセスするためのテーブル 
  Dictionary<string, AudioClip> mSEPool = new Dictionary<string, AudioClip>();

  public List<AudioClip> mBGMClip = new List<AudioClip>();
  public List<AudioClip> mSEClip = new List<AudioClip>();

  // サウンド再生のためのゲームオブジェクト
  GameObject _object = null;

  /// コンストラクタ
  public AudioMgr() {
    // チャンネル確保
    mSESourceArray = new AudioSource[SE_CHANNEL];

  }

  public override void Awake()
  {
      base.Awake();
      //オーディオリスナーを生成
      gameObject.AddComponent<AudioListener>();

      // AudioSourceを作成
      mBGMSource = gameObject.AddComponent<AudioSource>();
      mSESource = gameObject.AddComponent<AudioSource>();
      for (int i = 0; i < SE_CHANNEL; i++)
          mSESourceArray[i] = gameObject.AddComponent<AudioSource>();
  }
  /// AudioSourceを取得する
  AudioSource _GetAudioSource(eType type, int channel=-1) {
    
    if(type == eType.Bgm) {
      // BGM
      return mBGMSource;
    }
    else {
      // SE
      if (0 <= channel && channel < SE_CHANNEL)
      {
        // チャンネル指定
        return mSESourceArray[channel];
      }
      else
      {
        // デフォルト
        return mSESource;
      }
    }
  }

  // サウンドのロード
  // ※Resources/Soundsフォルダに配置すること
  public static void LoadBgm(string key, string resName) {
    instance._LoadBgm(key, resName);
  }
  public static void LoadSe(string key, string resName) {
      instance._LoadSe(key, resName);
  }
  void _LoadBgm(string key, string resName) {
    if (mBGMPool.ContainsKey(key))
    {
      // すでに登録済みなのでいったん消す
      mBGMPool.Remove(key);
    }
    mBGMPool.Add(key, Resources.Load("Sounds/BGM/" + resName) as AudioClip);
  }
  void _LoadSe(string key, string resName) {
    if (mSEPool.ContainsKey(key))
    {
      // すでに登録済みなのでいったん消す
      mSEPool.Remove(key);
    }
    mSEPool.Add(key,Resources.Load("Sounds/SE/" + resName) as AudioClip);
  }

  /// BGMの再生
  /// ※事前にLoadBgmでロードしておくこと
  public static bool PlayBgm(string key) {
    return instance._PlayBgm(key);
  }
  bool _PlayBgm(string key) {
    if(mBGMPool.ContainsKey(key) == false) {
      // 対応するキーがない
      return false;
    }

    // いったん止める
    _StopBgm();

    // リソースの取得
    //AudioClip clip = mBGMPool[key];

    // 再生
    var source = _GetAudioSource(eType.Bgm);
    source.loop = true;
    source.clip = mBGMPool[key];
    source.Play();

    return true;
  }
  /// BGMの停止
  public static bool StopBgm() {
    return instance._StopBgm();
  }
  bool _StopBgm() {
    _GetAudioSource(eType.Bgm).Stop();

    return true;
  }

  /// SEの再生
  /// ※事前にLoadSeでロードしておくこと
  public static bool PlaySe(string key, int channel=-1) {
    return instance._PlaySe(key, channel);
  }
  bool _PlaySe(string key, int channel=-1) {
    if(mSEPool.ContainsKey(key) == false) {
      // 対応するキーがない
      return false;
    }

    // リソースの取得
    //var _data = mSEPool[key];

    if (0 <= channel && channel < SE_CHANNEL)
    {
      // チャンネル指定
      var source = _GetAudioSource(eType.Se, channel);
      source.clip = mSEPool[key];
      source.Play();
    }
    else
    {
      // デフォルトで再生
      var source = _GetAudioSource(eType.Se);
      source.PlayOneShot(mSEPool[key]);
    }

    return true;
  }
}
