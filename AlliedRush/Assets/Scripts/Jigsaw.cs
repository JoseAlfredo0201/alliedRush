using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jigsaw : MonoBehaviour {
    [Header("Game Elements")]
    [Range(2, 6)]
    [SerializeField] private int difficulty = 4;
    [SerializeField] private Transform gameHolder;
    [SerializeField] private Transform piecePrefab;
    [SerializeField] private Texture2D puzzleTexture;
    [SerializeField] private AudioClip correctPlaceSound;
    [SerializeField] private string puzzleKey = "Puzzle2Solved";
    [SerializeField] private string returnScene = "Nivel 2";

    private AudioSource audioSource;
    private List<Transform> pieces;
    private Vector2Int dimensions;
    private float width;
    private float height;
    private Transform draggingPiece = null;
    private Vector3 offset;
    private int piecesCorrect;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        StartGame();
    }

    public void StartGame() {
        pieces = new List<Transform>();
        dimensions = GetDimensions(puzzleTexture, difficulty);
        CreateJigsawPieces(puzzleTexture);
        Scatter();
        UpdateBorder();
        piecesCorrect = 0;
    }

    Vector2Int GetDimensions(Texture2D tex, int diff) {
        Vector2Int dim = Vector2Int.zero;
        if (tex.width < tex.height) {
            dim.x = diff;
            dim.y = (diff * tex.height) / tex.width;
        } else {
            dim.x = (diff * tex.width) / tex.height;
            dim.y = diff;
        }
        return dim;
    }

    void CreateJigsawPieces(Texture2D tex) {
        height = 1f / dimensions.y;
        float aspect = (float)tex.width / tex.height;
        width = aspect / dimensions.x;

        for (int row = 0; row < dimensions.y; row++) {
            for (int col = 0; col < dimensions.x; col++) {
                Transform piece = Instantiate(piecePrefab, gameHolder);
                piece.localPosition = new Vector3(
                    (-width * dimensions.x / 2) + (width * col) + (width / 2),
                    (-height * dimensions.y / 2) + (height * row) + (height / 2),
                    -1);
                piece.localScale = new Vector3(width, height, 1f);
                piece.name = $"Piece {(row * dimensions.x) + col}";
                pieces.Add(piece);

                float u = 1f / dimensions.x;
                float v = 1f / dimensions.y;

                Vector2[] uv = {
                    new Vector2(u * col, v * row),
                    new Vector2(u * (col + 1), v * row),
                    new Vector2(u * col, v * (row + 1)),
                    new Vector2(u * (col + 1), v * (row + 1))
                };

                Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                mesh.uv = uv;
                piece.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", tex);
            }
        }
    }

    private void Scatter() {
        float orthoHeight = Camera.main.orthographicSize;
        float screenAspect = (float)Screen.width / Screen.height;
        float orthoWidth = screenAspect * orthoHeight;

        float pieceW = width * gameHolder.localScale.x;
        float pieceH = height * gameHolder.localScale.y;

        orthoHeight -= pieceH;
        orthoWidth -= pieceW;

        foreach (Transform piece in pieces) {
            float x = Random.Range(-orthoWidth, orthoWidth);
            float y = Random.Range(-orthoHeight, orthoHeight);
            piece.position = new Vector3(x, y, -1);
        }
    }

    private void UpdateBorder() {
        LineRenderer lr = gameHolder.GetComponent<LineRenderer>();
        float hw = (width * dimensions.x) / 2f;
        float hh = (height * dimensions.y) / 2f;
        float z = 0f;

        lr.SetPosition(0, new Vector3(-hw, hh, z));
        lr.SetPosition(1, new Vector3(hw, hh, z));
        lr.SetPosition(2, new Vector3(hw, -hh, z));
        lr.SetPosition(3, new Vector3(-hw, -hh, z));
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.enabled = true;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit) {
                draggingPiece = hit.transform;
                offset = draggingPiece.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                offset += Vector3.back;
            }
        }

        if (draggingPiece && Input.GetMouseButtonUp(0)) {
            SnapAndDisableIfCorrect();
            draggingPiece.position += Vector3.forward;
            draggingPiece = null;
        }

        if (draggingPiece) {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition += offset;
            draggingPiece.position = newPosition;
        }
    }

    private void SnapAndDisableIfCorrect() {
        int index = pieces.IndexOf(draggingPiece);
        int col = index % dimensions.x;
        int row = index / dimensions.x;
        Vector2 target = new Vector2(
            (-width * dimensions.x / 2) + (width * col) + (width / 2),
            (-height * dimensions.y / 2) + (height * row) + (height / 2));

        if (Vector2.Distance(draggingPiece.localPosition, target) < (width / 2)) {
            draggingPiece.localPosition = target;
            draggingPiece.GetComponent<BoxCollider2D>().enabled = false;
            piecesCorrect++;

            if (correctPlaceSound != null)
                audioSource.PlayOneShot(correctPlaceSound);

            if (piecesCorrect == pieces.Count) {
                PlayerPrefs.SetInt(puzzleKey, 1);
                PlayerPrefs.Save();
                SceneManager.LoadScene(returnScene);
            }
        }
    }
}