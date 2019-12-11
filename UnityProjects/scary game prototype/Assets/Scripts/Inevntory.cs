using System;
using UnityEngine;
using UnityEngine.UI;

public class Inventory {
    public bool init = false;
    
    public string _1 = "";
    public string _2 = "";
    public string _3 = "";
    public string _4 = "";
    public string[] In {
        get {
            return new string[4] { _1, _2, _3, _4 };
        }
        set {
            _1 = value[0];
            _2 = value[1];
            _2 = value[2];
            _3 = value[3];
        }
    }
    public Inventory(string A, string B, string C, string D) {
        _1 = A;
        _2 = B;
        _3 = C;
        _4 = D;

        init = true;
    }
    public Inventory(string A, string B, string C) {
        _1 = A;
        _2 = B;
        _3 = C;

        init = true;
    }
    public Inventory(string A, string B) {
        _1 = A;
        _2 = B;

        init = true;
    }
    public string Index(int i) {
        switch(i) {
            case 1:
                return this._1;
            case 2:
                return this._2;
            case 3:
                return this._3;
            case 4:
                return this._4;
            default:
                return "";
        }
    }
    public void SetIndex(int i,string set) {
        switch (i) {
            case 1:
                _1 = set;
                return;
            case 2:
                _2 = set;
                return;
            case 3:
                _3 = set;
                return;
            case 4:
                _4 = set;
                return;
            default:
                return;
        }
    }
    public Inventory(string A) {
        _1 = A;

        init = true;
    }
    public Inventory() {
        init = true;
    }
    public int Length() {
        int length = 0;
        for (int i = 0; i < 4; i++) {
            if(Index(i+1)!="") length++;
        }
        return length;
    }

    public Inventory Format() {
        Inventory outt = this;
        bool Formatted = true;
        for(int i = 0; i< 4; i++) {
            if(Index(i+2)==""&&Index(i+3)=="") {
                Formatted = false;
                break;
            } else {
                Formatted = true;
            }
            Debug.Log(Formatted);
        }
        for(int i = 0; i < 4; i++) {
            if(Index(i+1)=="") {
                if (i != 3) {
                    outt.SetIndex(i + 1, outt.Index(i + 2));
                    outt.SetIndex(i + 2, "");
                }
            }
        }
        Debug.Log($"{outt}");
        return !Formatted ? outt.Format() : outt;
    }
    public int FullLength = 4;
    public static Inventory operator +(Inventory a,string b) {
        //Get length
        //a = a.Format();
        //Debug.Log(a.Length());
        int l = a.Length();
        if(l==4) {
            return a;
        }
        switch(l) {
            case 0:
                return new Inventory(b);
            case 1:
                return new Inventory(a._1, b);
            case 2:
                return new Inventory(a._1, a._2, b);
            case 3:
                return new Inventory(a._1, a._2, a._3, b);
        }
        return a;
    }
    public static Inventory operator -(Inventory a,string b) {
        //a = a.Format();
        Inventory A = a;
        for(var i=0;i<4;i++) {
            if(A.Index(i+1)==b) {
                A.SetIndex(i + 1,"");
                break;
            }
        }
        //A = A.Format();
        return A;
    }
}

public class Inevntory : MonoBehaviour {
    public GameObject Importt;
    private Mitochondrian Import;
    private const float X = 0f;
    private const float X1 = 0.5f;
    private Inventory Inv = new Inventory();
    public GameObject Slot1;
    public GameObject Slot2;
    public GameObject Slot3;
    public GameObject Slot4;
    public Texture2D CO2;
    public Texture2D H2O;
    public Texture2D Default;
    public GameObject Crosshair;
    public float Radius;
    // Start is called before the first frame update
    void Start() {
        Import = Importt.transform.GetComponent<Mitochondrian>();
        Crosshair.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        Transform transform1 = gameObject.transform;

        //Take items
        var Rad = TakeItems(transform1);

        //Put items
        PutItems(transform1,Rad);
    }

    private void PutItems(Transform transform1, bool rad) {
        bool Rad = rad;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Import");
        GameObject obj = gameObject; //Give object dummy value to pass compiler
        foreach (GameObject i in objs) {
            if ((transform1.position - i.transform.position).magnitude < Radius) {
                Rad = true;
                obj = i;
            }
        }

        Crosshair.SetActive(Rad);

        if (Rad) {
            //Debug.Log(Rad);
            if (Input.GetButtonDown("Fire3")) {
                //Debug.Log(Input.GetButtonDown("Fire2"));
                for (int i = 0; i < 4; i-=-1) {
                    var type = Inv.In[i];
                    switch(type) {
                        case "H2O":
                            if(!Import.M.h2o.Met) { 
                                Inv = Inv - "H2O";
                                Import.M.h2o.Num++;
                            }
                            break;
                        case "CO2":
                            if (!Import.M.co2.Met) {
                                Inv = Inv - "CO2";
                                Import.M.co2.Num++;
                            }
                            break;
                    }
                }
            }
        }
    }

    bool TakeItems(Transform transform1) {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Item");
        bool Rad = false;
        GameObject obj = gameObject; //Give object dummy value to pass compiler
        foreach (GameObject i in objs) {
            if ((transform1.position - i.transform.position).magnitude < Radius && Inv.Length()<4) {
                Rad = true;
                obj = i;
            }
        }

        Crosshair.SetActive(Rad);

        if (Rad) {
            //If there is an item within radius
            if (Input.GetButtonDown("Fire2")) {
                Inv = Inv + obj.name;
                obj.SetActive(false);
                //Update inventory
                UpdateInventory(Inv);
            }
        }
        return Rad;
    } 

    private void UpdateInventory(Inventory Inve) {
        string[] I = Inve.In;
        for(int i=0;i<4;i++) {
            switch(I[i]) {
                case "":
                    Match(i, Default);
                    break;
                case "H2O":
                    Match(i, H2O);
                    break;
                case "CO2":
                    Match(i, CO2);
                    break;
            }
        }
    }

    private void Match(int Slot,Texture2D Img) {
        int height = Img.height;
        switch (Slot) {
            case 0:
                Slot1.GetComponent<Image>().sprite = Sprite.Create(Img, new Rect(X, X, Img.width, height), new Vector2(X1, X1));
                break;
            case 1:
                Slot2.GetComponent<Image>().sprite = Sprite.Create(Img, new Rect(X, X, Img.width, height), new Vector2(X1, X1));
                break;
            case 2:
                Slot3.GetComponent<Image>().sprite = Sprite.Create(Img, new Rect(X, X, Img.width, height), new Vector2(X1, X1));
                break;
            case 3:
                Slot4.GetComponent<Image>().sprite = Sprite.Create(Img, new Rect(X, X, Img.width, height), new Vector2(X1, X1));
                break;
            default:
                throw new Exception("This shoudlnt happeen!!!!!!!!");
        }
    }
}
