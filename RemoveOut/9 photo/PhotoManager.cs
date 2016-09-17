using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class PhotoManager : MonoBehaviour {

	public Sprite[] sprites;
	public AudioClip audioClip;
	public AudioSource audio;
	
	public AudioClip loopClip;
	public AudioSource loopAudio;
	List<SpriteRenderer> sprs;
	List<Vector3> positions;
	public static PhotoManager instance;
	
	void Awake()
	{
		instance = this;
		sprs = new List<SpriteRenderer>();
		positions = new List<Vector3>();
		lockFlags = new bool[9];
		
		for(int j=-1;j<=1;j++)
		for(int i=-1;i<=1;i++)
		{
			GameObject gobj = new GameObject("photo"+i+j);
			gobj.transform.position = new Vector3(i*5.4f,j*4f);
			gobj.AddComponent<BoxCollider2D>().size = new Vector2(4,3);
			
			positions.Add(new Vector3(i*5.4f,j*4f));
			sprs.Add(gobj.AddComponent<SpriteRenderer>());
			gobj.AddComponent<Photo>().init(sprs.Count-1);
				
		}
		for(int i=0;i<9;i++)
			sprs[i].sprite = sprites[i];
			
		
		
		InvokeRepeating("AllRandom",0,0.5f);
	}
	public void PlayLoop(int id)
	{
		isReadyToPlayLoop = true;
	}
	public void StopLoop(int id)
	{
		isReadyToPlayLoop = false;
	}
	bool isReadyToPlayLoop;
	bool isPlayingLoop =false;
	float timer =0;
	float interval = 0.5f;
	bool isAutoPlay = true;
	int t=0;
	void Update()
	{
		/*
		if(isAutoPlay)
		{
			if(timer==0)
			{
				AllRandom();
			}
			timer+=Time.deltaTime;
			if(timer>interval)
			{
				t++;
				//RandomSwap();
				timer = 0;
			}
			
		}
		*/
	}
	
	int[] randomNums;
	int[] lockPos;
	int[] oriNums;
	void RandomSwap()
	{
		oriNums = new int[]{0,1,2,3,5,6,7,8};
		randomNums = (int[])oriNums.Clone();
		
		/*
		for(int i=0;i<oriNums.Length;i++)
		{
			LeanTween.alpha(sprs[i].gameObject,0,0.5f).setLoopPingPong();
				//randomNums[i] = i;
		}
		*/
		
		for(int i=0;i<10;i++)//10 time random
		{
			int r1 = Random.Range(0,randomNums.Length);
			int r2 = Random.Range(0,randomNums.Length);
			
			if(r1!=r2)
			{
				int temp = randomNums[r1];
				randomNums[r1] = randomNums[r2];
				randomNums[r2] = temp;
			}
		}
		
		Invoke("ChangePhoto",0.5f);
		
	}
	List<int> numlist;
	int firstUnlock;
	void buildNumList(int l)
	{
		firstUnlock = -1;
		numlist = new List<int>();
		for(int i=0;i<l;i++)
		{
			if(!lockFlags[i])
			{
				if(firstUnlock==-1)
				firstUnlock = i;
				numlist.Add(i);
			}
			
		}
	}
	bool[] lockFlags;
	public void LockPhoto(int i)
	{
		lockFlags[i] = true;
		
	}
	public void UnlockPhoto(int i)
	{
		lockFlags[i] = false;
	}
	void AllRandom()
	{
		if(isReadyToPlayLoop&&isPlayingLoop==false)
		{
			isPlayingLoop = true;
			loopAudio.Play();
			
		}
		else if(isReadyToPlayLoop==false&&isPlayingLoop==true)
		{
			isPlayingLoop = false;
			loopAudio.Stop();
		}
		audio.PlayOneShot(audioClip);
		//build numlist form 0 to 8
		buildNumList(9);
		
		//LockPhoto(1);
		randomNums = new int[9];
		
		
		for(int i=0;i<9;i++)
		{
			if(lockFlags[i]==false)
			{
				bool isExist = numlist.Remove(i);
				if(numlist.Count==0)
				{
					randomNums[i] = randomNums[firstUnlock];
					randomNums[firstUnlock]=i;
					continue;
				}
				int r = Random.Range(0,numlist.Count);
				int num = numlist[r];
				if(i==num)
					Debug.Log("wrong");
				randomNums[i] = num;
				
				numlist.RemoveAt(r);
				if(isExist)
					numlist.Add(i);
			}
			else
			{
				randomNums[i] = i;
			}
			
			/*
		 	int temp = randomNums[i];
		 	randomNums[i] = randomNums[r];
		 	randomNums[r] = temp;*/
		}
		
		
		for(int i=0;i<randomNums.Length;i++)
		{
			//sprs[i].transform.position = positions[randomNums[i]];
			if(lockFlags[i]==false)
			LeanTween.move(sprs[i].transform.gameObject,positions[randomNums[i]],interval/2).setEase(LeanTweenType.easeInOutCirc);
			//sprs[i].sprite = sprites[randomNums[i]];
		}	
		Vector3[] tempPos = new Vector3[9];
		positions.CopyTo(tempPos);
		for(int i=0;i<randomNums.Length;i++)
		{
			
			positions[i] = tempPos[randomNums[i]];
			//positions[i] = new Vector3(positions[i].x,positions[i].y,Random.Range(-2,1));
		}
		
		
	}
	void SwapArray(int[] array, int s1,int s2)
	{
		int temp = array[s1];
		array[s1] = array[s2];
		array[s2] = temp;
	}
	/*
	void ChangePhoto()
	{
		for(int i=0;i<oriNums.Length;i++)
		{
			LeanTween.move(sprs[randomNums[i]].transform.gameObject,positions[oriNums[i]],0.2f);
			sprs[randomNums[i]].transform.position = ;
			
		}	
		
		for(int i=0;i<oriNums.Length;i++)
		{
			positions[i] = sprs[i].transform.position;
		}
}*/
	public void SwapSingle(int id)
	{
		//Random.Range(0,)
	}
	public void DoSingleChange()
	{
		AllRandom();
	}
	public void TogglePause()
	{
		isAutoPlay = !isAutoPlay;
	}
	public void SetSpeed(GameObject slider)
	{
		Slider sd = slider.GetComponent<Slider>();
		
		interval = sd.value;
	}
}
