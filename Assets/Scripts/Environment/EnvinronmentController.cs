using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvinronmentController : MonoBehaviour
{
    public float objectSpeed = 0;
    AudioSource _music;

    //public int sampleSize = 512;
    public float maxJump = 4f;
    public float HeightAdjust = 4f;
    public float VocalRangeScale = 1f;

    //public int musicBPM = 130;
    //public float spawnTimer = 2f;
    //private float lowestPoint = 0;
    //private float highestPoint = 0;

    private int avarageLoudest = 0;
    private int timeToSpawn = 0;

    private float lastHeight = 0;
    private GameObject lastObject;

    //private float lastSpawned = 0;
    //private int spawnCube = 0;
    //private int avarageSpawnest = 0;

    public Sprite sprite0;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;
    public Sprite sprite6;
    public Sprite sprite7;

    //private GameObject line;
    //private LineRenderer lineRenderer;

    void Start()
    {
        _music = GetComponent<AudioSource>();
        _music.time = objectSpeed;

        /*line = new GameObject();
        lineRenderer = line.AddComponent<LineRenderer>();*/

        //lowestPoint = sampleSize;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ListenToMusic();
    }

    private void ListenToMusic()
    {
        //float newLowest = sampleSize;
        //float newHighest = 0;

        float[] samples = new float[512];

        _music.GetSpectrumData(samples, 0, FFTWindow.Blackman);

        /*int count = 0;

        float[] freqBand = new float[8];
        for (int i = 0; i < 8; i++)
        {
            float avarage = 0;
            int sampleCount = (int)Math.Pow(2, i) * 2;

            if (i == 7)
                sampleCount += 2;
            
            for(int j = 0; j < sampleCount; j++)
            {
                avarage += samples[count] * (count + 1);
                count++;
            }

            avarage /= count/2;

            freqBand[i] = avarage * 10;
        }*/

        /*for(int i = 0; i < sampleSize; i++)
        {
            //Debug.Log("samples[" + i + "]: " + samples[i]);

            if (samples[i] > 0.001f)
            {
                newLowest = i;
                break;
            }
        }

        for (int i = sampleSize-1; i >= 0; i--)
        {
            //if (i == 511)
            //    Debug.Log("samples[" + i + "]: " + samples[i]);

            if (samples[i] > 0.001f)
            {
                newHighest = i;
                //Debug.Log("samples[" + i + "]: " + samples[i]);
                break;
            }
        }*/
        /*if (newLowest < lowestPoint)
        {
            //Debug.Log("Lowest point: " + newLowest);
            lowestPoint = newLowest;
        }

        if (newHighest > highestPoint)
        {
            //Debug.Log("Highest point: " + newHighest);
            highestPoint = newHighest;
        }*/
        //if(loudestIndex > 6)
        //    Debug.Log("loudestIndex: " + loudestIndex + ", LoudePoint: " + loudestPoint);

        timeToSpawn++;

        /*float spawnestPoint = 0;
        int tmpSpawnestPoint = 0;
        for (int i = 0; i < 8; i++)
        {
            if (freqBand[i]*(i+1) > spawnestPoint)
            {
                spawnestPoint = freqBand[i];
                tmpSpawnestPoint = i;
            }
        }

        avarageSpawnest += tmpSpawnestPoint;
        spawnCube = avarageSpawnest / timeToSpawn;*/

        // Lets count the loudest index of audios spectrum-data
        float loudestPoint = 0;
        int loudestIndex = 0;
        for (int i = 0; i < samples.Length; i++)
        {
            if (samples[i] > loudestPoint)
            {
                loudestPoint = samples[i];
                loudestIndex = i;
            }
        }

        avarageLoudest += loudestIndex;

        /*var linePos = new Vector3(1, loudestIndex, 0);

        for(int l = 0; l < lineRenderer.positionCount; l++) // (var pos in lineRenderer.GetPositions())
        {
            //lineRenderer.GetPosition(l).Set(lineRenderer.GetPosition(l).x - (Time.deltaTime * objectSpeed), lineRenderer.GetPosition(l).y, lineRenderer.GetPosition(l).z); //.Translate(Vector2.left * Time.deltaTime * objectSpeed);
            Vector3 newLinePos = new Vector3(lineRenderer.GetPosition(l).x - (Time.deltaTime * objectSpeed), lineRenderer.GetPosition(l).y, lineRenderer.GetPosition(l).z);
            lineRenderer.SetPosition(l, newLinePos);
        }

        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, linePos);*/

        //lineRenderer.transform.Translate(Vector2.left * Time.deltaTime * objectSpeed);
        //line.transform.Translate(Vector2.left * Time.deltaTime * objectSpeed);

        //if(avarageLoudest < loudestIndex)

        //if (timeToSpawn == 200)
        //Debug.Log("Time.time: " + Time.time);
        //if (Time.time - spawnTimer > lastSpawned)

        // if its time to spawn new object (last object is close to the edge of the screen)
        if (lastObject == null || lastObject.transform.position.x < 12)
        {
            CreateNewCube(avarageLoudest / timeToSpawn);
            timeToSpawn = 0;
            avarageLoudest = 0;
            //lastSpawned = Time.time;
            //spawnCube = 0;
            //avarageSpawnest = 0;
        }
    }

    private void CreateNewCube(float height)
    {
        height *= VocalRangeScale;
        height += HeightAdjust;
        Debug.Log("height: " + height);

        float spawnX = 14.5f;

        if (lastObject != null)
        {
            // Last objects width + right edge
            float width = lastObject.GetComponent<SpriteRenderer>().bounds.size.x;
            float lastX = lastObject.transform.position.x + width/2;

            // If last object was higher
            if (lastHeight >= height)
                spawnX = lastX + maxJump;
            else
            {
                // Else we calculate the hypotenuse so we know the max distance
                // this would be better to change somehow
                float hypo = (float)Math.Sqrt(Math.Pow(maxJump/2, 2) + Math.Pow(height - lastHeight, 2));
                Debug.Log("hypo: " + hypo + ", maxJump: " + maxJump);

                if (hypo < maxJump)
                    spawnX = lastX + maxJump;
                else
                {
                    float percent = maxJump / (hypo);
                    Debug.Log("percent: " + percent);
                    spawnX = lastX + maxJump * percent;
                    height = lastHeight + (height - lastHeight)*percent;
                }
            }
        }

        // Create new object
        GameObject cube = new GameObject();

        var spriteRenderer = cube.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = ReturnSprite((int)height);
        Debug.Log("height: " + height);

        if (height < -3)
            height = -2;

        cube.transform.position = new Vector3(spawnX, -spriteRenderer.size.y/2 + height, 0);
        cube.tag = "Platform";

        var rigidBody = cube.AddComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0;
        rigidBody.freezeRotation = true;
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        //rigidBody.isKinematic = false;
        cube.AddComponent<BoxCollider2D>();
        cube.AddComponent<EnvironmentObject>();

        // Save object for calculations
        lastObject = cube;
        lastHeight = height;
        //cube.transform.localScale = scaleChange;
        //var sprite = Resources.Load<Sprite>("../../Graphics/piano4keyrev");
        //var texture = Resources.Load<Texture2D>("/Graphics/piano4keyrev");
        //var sprite = Sprite.Create(block, new Rect(0, 0, 32, 32), new Vector2(16, 16));
    }

    private Sprite ReturnSprite(int height)
    {
        Sprite retVal;

        if (height <= -2)
            retVal = sprite0;
        else if (height <= 0)
            retVal = sprite1;
        else if (height <= 2)
            retVal = sprite2;
        else if (height <= 3)
            retVal = sprite3;
        else if (height <= 4)
            retVal = sprite4;
        else if (height <= 5)
            retVal = sprite5;
        else if (height <= 6)
            retVal = sprite6;
        else if (height > 6)
            retVal = sprite7;
        else
            retVal = sprite0;

        return retVal;
    }
}
