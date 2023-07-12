using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    /// <summary>
    /// A class that loads a scene after fading out when calling GoToSceneAsync and passing the Scene Index.
    /// Fades in from Black on Start. 
    /// 
    /// Notes:
    /// Currently relies on there being a instance in each scene, good for OOP, bad for flexibilty,
    /// would be good to make it a static class somehow or use Scriptable Objects but for now this will do.
    /// </summary>
    
    public bool fadeOnStart = true;
    public float fadeDuration = 2;
    public Color fadeColor;
    public GameObject FadeSphereGameObject;
    private GameObject _GameObject;
    private bool _FadingIn;
    private bool _CoroutineRunning;

    private void Start()
    {
        if (fadeOnStart) { FadeIn(); }
    }

    #region Public Functions

    public void GoToSceneAsync(int sceneIndex)
    {
        StartCoroutine(GoToSceneAsyncRoutine(sceneIndex));
    }

    public void FadeIn()
    {
        _FadingIn = true;
        Fade(1, 0);
    }

    public void FadeOut()
    {
        _FadingIn = false;
        Fade(0, 1);
    }

    #endregion

    #region Private Functions

    private void Fade(float alphaIn, float alphaOut)
    {
        if(_CoroutineRunning){ StopAllCoroutines();}
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    // Scene Loading Function
    private IEnumerator GoToSceneAsyncRoutine(int sceneIndex)
    {
        FadeOut();
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        float timer = 0;
        while (timer <= fadeDuration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        operation.allowSceneActivation = true;
        yield return operation.isDone;
        Destroy(_GameObject);
    }

    // Main Fade Function
    private IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        _CoroutineRunning = true;
        if (_FadingIn) { Destroy(_GameObject); }
        float timer = 0;
        // FadeSphere = CreateFadeSphereObject();
        _GameObject = Instantiate(FadeSphereGameObject, Camera.main.transform);
        Material material = _GameObject.GetComponent<Renderer>().material;
        while (timer <= fadeDuration)
        {
            fadeColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);
            timer += Time.deltaTime;
            material.color = fadeColor;
            yield return null;
        }
        if (_FadingIn) { Destroy(_GameObject); }

        _CoroutineRunning = false;
    }


    // Old code, Hasn't working as planned due to seting the _surface mode at runtime
    // 
    /*    private Material SetMaterial(GameObject obj)
        {
            Material material = obj.GetComponent<Renderer>().material;
            material.SetFloat("_Cull", (float)CullMode.Off);
            // 1.0f is equivilant to "Transparent" in "Universal Render Pipeline/Unlit"
            material.SetFloat("_Surface", 1.0f);
            material.shader = Shader.Find("Universal Render Pipeline/Unlit");
            return material;
        }

        private GameObject CreateFadeSphereObject()
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            obj.transform.parent = Camera.main.transform;
            Destroy(obj.GetComponent<Collider>());
            Renderer renderer = obj.GetComponent<Renderer>();
            renderer.material = SetMaterial(obj);
            if(renderer == null)
            {
                obj.AddComponent<Renderer>();
            }
            return obj;
        }*/

    #endregion

}
