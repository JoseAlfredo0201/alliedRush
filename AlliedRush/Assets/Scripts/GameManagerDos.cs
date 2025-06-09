using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerDos : MonoBehaviour
{
    [Header("Puzzle Settings")]
    public string puzzleKey = "Puzzle1Solved";      // Set this uniquely per puzzle scene
    public string returnSceneName = "Nivel";        // Scene to return to after solving

    [SerializeField] private Transform gameTransform;
    [SerializeField] private Transform piecePrefab;
    [SerializeField] private AudioSource moveSound;

    private List<Transform> pieces;
    private int emptyLocation;
    private int size;
    private bool puzzleSolved = false;

    private void CreateGamePieces(float gapThickness)
    {
        float width = 1f / size;
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Transform piece = Instantiate(piecePrefab, gameTransform);
                pieces.Add(piece);
                piece.localPosition = new Vector3(-1 + (2 * width * col) + width,
                                                  +1 - (2 * width * row) - width,
                                                  0);
                piece.localScale = ((2 * width) - gapThickness) * Vector3.one;
                piece.name = $"{(row * size) + col}";

                if (row == size - 1 && col == size - 1)
                {
                    emptyLocation = (size * size) - 1;
                    piece.gameObject.SetActive(false);
                }
                else
                {
                    float gap = gapThickness / 2;
                    Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                    Vector2[] uv = new Vector2[4];
                    uv[0] = new Vector2((width * col) + gap, 1 - ((width * (row + 1)) - gap));
                    uv[1] = new Vector2((width * (col + 1)) - gap, 1 - ((width * (row + 1)) - gap));
                    uv[2] = new Vector2((width * col) + gap, 1 - ((width * row) + gap));
                    uv[3] = new Vector2((width * (col + 1)) - gap, 1 - ((width * row) + gap));
                    mesh.uv = uv;
                }
            }
        }
    }

    private bool allowCompletionCheck = false; // NEW

    void Start()
    {
        pieces = new List<Transform>();
        size = 3;
        CreateGamePieces(0.01f);
        StartCoroutine(ShuffleAfterDelay(0.5f));
    }

    private IEnumerator ShuffleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Shuffle();
        allowCompletionCheck = true; // ✅ allow checking only AFTER shuffling
    }

    void Update()
    {
        if (allowCompletionCheck && !puzzleSolved && CheckCompletion())
        {
            puzzleSolved = true;
            PlayerPrefs.SetInt(puzzleKey, 1);
            PlayerPrefs.Save();
            Debug.Log($"Puzzle solved!");

            StartCoroutine(GoBackToLevelAfterDelay(1.5f));
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                for (int i = 0; i < pieces.Count; i++)
                {
                    if (pieces[i] == hit.transform)
                    {
                        if (SwapIfValid(i, -size, size)) { PlayMoveSound(); break; }
                        if (SwapIfValid(i, +size, size)) { PlayMoveSound(); break; }
                        if (SwapIfValid(i, -1, 0)) { PlayMoveSound(); break; }
                        if (SwapIfValid(i, +1, size - 1)) { PlayMoveSound(); break; }
                    }
                }
            }
        }
    }


    private void PlayMoveSound()
    {
        if (moveSound != null)
        {
            moveSound.Play();
        }
    }

    private bool SwapIfValid(int i, int offset, int colCheck)
    {
        if (((i % size) != colCheck) && ((i + offset) == emptyLocation))
        {
            (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);
            (pieces[i].localPosition, pieces[i + offset].localPosition) = (pieces[i + offset].localPosition, pieces[i].localPosition);
            emptyLocation = i;
            return true;
        }
        return false;
    }

    private bool CheckCompletion()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].name != $"{i}")
                return false;
        }
        return true;
    }

    private void Shuffle()
    {
        int count = 0;
        int last = -1;
        while (count < (size * size * size))
        {
            int rnd = Random.Range(0, size * size);
            if (rnd == last) continue;
            last = emptyLocation;

            if (SwapIfValid(rnd, -size, size)) count++;
            else if (SwapIfValid(rnd, +size, size)) count++;
            else if (SwapIfValid(rnd, -1, 0)) count++;
            else if (SwapIfValid(rnd, +1, size - 1)) count++;
        }
    }

    private IEnumerator GoBackToLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(returnSceneName); // ✅ Go back to Nivel
    }
}