using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {
    public Animator mainMenu;
    public Animator marbleSelect;
    public Animator mazePage;
    public Camera myCamera;
    public Text mazeTitleText;
    public GameObject marbleSelectObjects;


    private GameObject currentMaze;
    private int selectedMarble = 0;
    private int state = -1;
    private int mazeIndex = 0;


    public void Start() {
        mainMenu.SetBool("showing", true);
        marbleSelectObjects.SetActive(false);
    }

    public void Update() {
        if (state == 0) { // Marble select
            if (Input.GetAxis("Fire1") > 0) {
                Vector3 point = myCamera.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(point, Vector2.right, .001f);
                MarbleSelector m;
                if (hit && (m = hit.transform.GetComponent<MarbleSelector>())) {
                    selectedMarble = m.index;
                    print(selectedMarble);
                    m.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 60);
                    NextPage(1);
                }
            }
        }
        
    }

    IEnumerator disableObject(GameObject obj) {
        yield return new WaitForSeconds(1);
        obj.SetActive(false);
        yield return null;
    }

    public void NextPage(int currentPage) {
        mainMenu.SetBool("showing", false);
        marbleSelect.SetBool("showing", false);
        mazePage.SetBool("showing", false);

        state = currentPage;
        switch (currentPage) {
            case 0:
                marbleSelect.SetBool("showing", true);
                marbleSelectObjects.SetActive(true);
                break;
            case 1:
                StartCoroutine(disableObject(marbleSelectObjects));
                NextMaze();
                break;
        }
    }

    public void NextMaze() {

        if (currentMaze != null) {
            GameObject.Destroy(currentMaze);
        }
        mazeIndex++;

        mazePage.SetBool("showing_fadeback", true);
        mazeTitleText.text = "Maze #" + mazeIndex;

        // TODO generate maze

        // TODO set maze parent as currentMaze
    }
}
