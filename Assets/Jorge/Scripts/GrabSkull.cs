using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabSkull : MonoBehaviour
{
    [SerializeField] private GameObject _skullHand;
    [SerializeField] private GameObject _hookHand;
    [SerializeField] private GameObject _pokeInteractor;
    [SerializeField] private GameObject _teleportInteractor;
    [SerializeField] private GameObject _rayInteractor;
    [SerializeField] private GameObject _teleportLocomotion;

    public void GrabSkullAction()
    {
        Destroy(this.gameObject);
        _hookHand.SetActive(false);
        _skullHand.SetActive(true);
        _skullHand.GetComponent<AudioSource>().Play();
        //_teleportInteractor.SetActive(true);
        _teleportInteractor.GetComponent<XRRayInteractor>().enabled = true;
        _rayInteractor.SetActive(true);
        _rayInteractor.GetComponent<XRRayInteractor>().enabled = true;
        _teleportLocomotion.SetActive(true);
        _pokeInteractor.GetComponent<CustomInputAction>().enabled = false;
    }
}
