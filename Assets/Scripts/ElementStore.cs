using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementStore : MonoBehaviour {

    public static List<Element> H_BEAM,I_BEAM;
    public static List<UElement> U_PROP;
    public static List<PElement> PIPE;
    public static List<AElement> UT_PROP; 
    public struct UElement
    {
        public float E, I;
        public UElement(float E,float I)
        {
            this.E = E;
            this.I = I;
        }
    }
    public struct Element
    {
        public string name;
        public float w,h,b,t1,t2,r,area,ix,iy,rx,ry,zx,zy,k,e,fb,cb,fy,bf,lx,rt,c,rc;
        public Element(string name,float w,float h,float b,float t1,float t2,float r,float area,float ix,float iy,float rx,float ry,float zx,float zy,float k,float e,float fb,float cb,float fy,float bf,float lx,float rt,float c,float rc)
        {
            this.name = name;
            this.w = w;
            this.h = h;
            this.b = b;
            this.t1 = t1;
            this.t2 = t2;
            this.r = r;
            this.area = area;
            this.ix = ix;
            this.iy = iy;
            this.rx = rx;
            this.ry = ry;
            this.zx = zx;
            this.zy = zy;
            this.k = k;
            this.e = e;
            this.fb = fb;
            this.cb = cb;
            this.fy = fy;
            this.bf = bf;
            this.lx = lx;
            this.rt = rt;
            this.c = c;
            this.rc = rc;
        }
    }

    public struct PElement
    {
        public string name;
        public float outsideDiameter, thickness, weight, area, i, r, z, fb, fy, k;
        public PElement(string name,float outsideDiameter,float thickness,float weight,float area,float i,float r,float z,float fb,float fy,float k)
        {
            this.name = name;
            this.outsideDiameter = outsideDiameter;
            this.thickness = thickness;
            this.weight = weight;
            this.area = area;
            this.i = i;
            this.r = r;
            this.z = z;
            this.fb = fb;
            this.fy = fy;
            this.k = k;
        }
    }

    public struct AElement
    {
        public float E, A;
        public AElement(float E,float A)
        {
            this.E = E;
            this.A = A;
        }
    }

    // Use this for initialization
    void Start () {
        GenerateH();
        GenerateI();
        GenerateU();
        GeneratePIPE();
        GenerateUT();
    }

    void GenerateH()
    {
        Debug.Log("Gen H");
        H_BEAM = new List<Element>();
        H_BEAM.Add(new Element("100mmX50mmX9.3kg", 9.3f, 100f, 50f, 5f, 7f, 8f, 11.85f, 187f, 14.8f, 3.98f, 1.12f, 37.5f, 5.91f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.05f, 0.00000187f, 0.013f, 0.05f, 0.0111756214488472f));
        H_BEAM.Add(new Element("100mmX100mmX17.2kg", 17.2f, 100f, 100f, 6f, 8f, 10f, 21.9f, 383f, 134f, 4.18f, 2.47f, 76.5f, 26.7f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.1f, 0.00000383f, 0.026f, 0.05f, 0.0247360495253127f));
        H_BEAM.Add(new Element("125mmX125mmX23.8kg", 23.8f, 125f, 125f, 6.5f, 9f, 10f, 30.31f, 847f, 293f, 5.29f, 3.11f, 136f, 47f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.125f, 0.00000847f, 0.0325f, 0.0625f, 0.031091440367684f));
        H_BEAM.Add(new Element("150mmX75mmX14kg", 14f, 150f, 75f, 5f, 7f, 8f, 17.85f, 666f, 49.5f, 6.11f, 1.66f, 88.8f, 13.2f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.075f, 0.00000666f, 0.0195f, 0.075f, 0.0166526551747686f));
        H_BEAM.Add(new Element("148mmX100mmX21.1kg", 21.1f, 148f, 100f, 6f, 9f, 11f, 26.84f, 1020f, 151f, 6.17f, 2.37f, 138f, 30.1f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.1f, 0.0000102f, 0.026f, 0.074f, 0.023719046029728f));
        H_BEAM.Add(new Element("150mmX150mmX31.5kg", 31.5f, 150f, 150f, 7f, 10f, 11f, 40.14f, 1640f, 563f, 6.39f, 3.75f, 219f, 75.1f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.15f, 0.0000164f, 0.039f, 0.075f, 0.0374511806454605f));
        H_BEAM.Add(new Element("175mmX175mmX40.2kg", 40.2f, 175f, 175f, 7.5f, 11f, 12f, 51.21f, 2880f, 984f, 7.5f, 4.38f, 330f, 112f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.175f, 0.0000288f, 0.0455f, 0.0875f, 0.0438349142475317f));
        H_BEAM.Add(new Element("200mmX100mmX21.3kg", 21.3f, 200f, 100f, 5.5f, 8f, 11f, 27.16f, 1840f, 134f, 8.24f, 2.22f, 184f, 26.8f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.1f, 0.0000184f, 0.026f, 0.1f, 0.0222119924089369f));
        H_BEAM.Add(new Element("194mmX150mmX30.6kg", 30.6f, 194f, 150f, 6f, 9f, 13f, 39.01f, 2690f, 507f, 8.3f, 3.61f, 277f, 67.6f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.15f, 0.0000269f, 0.039f, 0.097f, 0.0360508911417574f));
        H_BEAM.Add(new Element("200mmX200mmX49.9kg", 49.9f, 200f, 200f, 8f, 12f, 13f, 63.53f, 4720f, 1600f, 8.62f, 5.02f, 472f, 160f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.2f, 0.0000472f, 0.052f, 0.1f, 0.0501846111783137f));
        H_BEAM.Add(new Element("250mmX125mmX39.6kg", 39.6f, 250f, 125f, 6f, 9f, 12f, 37.66f, 4050f, 294f, 10.4f, 2.79f, 324f, 47f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.125f, 0.0000405f, 0.0325f, 0.125f, 0.0279404571362283f));
        H_BEAM.Add(new Element("244mmX175mmX44.1kg", 44.1f, 244f, 175f, 7f, 11f, 16f, 56.24f, 6120f, 984f, 10.4f, 4.18f, 502f, 113f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.175f, 0.0000612f, 0.0455f, 0.122f, 0.0418287506533883f));
        H_BEAM.Add(new Element("250mmX250mmX72.4kg", 72.4f, 250f, 250f, 9f, 14f, 16f, 92.18f, 10800f, 3650f, 10.8f, 6.29f, 867f, 292f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.25f, 0.000108f, 0.065f, 0.125f, 0.0629257036070421f));
        H_BEAM.Add(new Element("300mmX150mmX36.7kg", 36.7f, 300f, 150f, 6.5f, 9f, 13f, 46.78f, 7210f, 508f, 12.4f, 3.29f, 481f, 67.7f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.15f, 0.0000721f, 0.039f, 0.15f, 0.0329535151371958f));
        H_BEAM.Add(new Element("294mmX200mmX56.8kg", 56.8f, 294f, 200f, 8f, 12f, 18f, 72.38f, 1130f, 1600f, 12.5f, 4.71f, 771f, 160f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.2f, 0.0000113f, 0.052f, 0.147f, 0.0470165439185481f));
        H_BEAM.Add(new Element("300mmX300mmX94kg", 94f, 300f, 300f, 10f, 15f, 18f, 119.8f, 20400f, 6750f, 13.1f, 7.51f, 1360f, 450f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.3f, 0.000204f, 0.078f, 0.15f, 0.0750625782336654f));
        H_BEAM.Add(new Element("350mmX175mmX49.6kg", 49.6f, 350f, 175f, 7f, 11f, 14f, 63.14f, 13600f, 984f, 14.7f, 3.95f, 775f, 112f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.175f, 0.000136f, 0.0455f, 0.175f, 0.0394771016975861f));
        H_BEAM.Add(new Element("340mmX250mmX79.7kg", 79.7f, 340f, 250f, 9f, 14f, 20f, 101.5f, 21700f, 3650f, 14.6f, 6f, 1280f, 292f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.25f, 0.000217f, 0.065f, 0.17f, 0.0599671502849726f));
        H_BEAM.Add(new Element("350mmX350mmX137kg", 137f, 350f, 350f, 12f, 19f, 20f, 173.9f, 40300f, 13600f, 15.2f, 8.84f, 2300f, 776f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.35f, 0.000403f, 0.091f, 0.175f, 0.0884340802179273f));
        H_BEAM.Add(new Element("400mmX200mmX66kg", 66f, 400f, 200f, 8f, 13f, 16f, 84.12f, 23700f, 1740f, 16.8f, 4.54f, 1190f, 174f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.2f, 0.000237f, 0.052f, 0.2f, 0.0454804750319279f));
        H_BEAM.Add(new Element("390mmX300mmX107kg", 107f, 390f, 300f, 10f, 16f, 22f, 136f, 38700f, 7210f, 16.9f, 7.28f, 1980f, 481f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.3f, 0.000387f, 0.078f, 0.195f, 0.0728111982337559f));
        H_BEAM.Add(new Element("400mmX400mmX172kg", 172f, 400f, 400f, 13f, 21f, 22f, 218.7f, 66600f, 22400f, 17.5f, 10.1f, 3330f, 1120f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.4f, 0.000666f, 0.104f, 0.2f, 0.101204452009478f));
        H_BEAM.Add(new Element("450mmX200mmX76kg", 76f, 450f, 200f, 9f, 14f, 18f, 69.76f, 33500f, 1870f, 16.8f, 4.4f, 1490f, 187f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.2f, 0.000335f, 0.052f, 0.225f, 0.0517746971604378f));
        H_BEAM.Add(new Element("440mmX300mmX124kg", 124f, 440f, 300f, 11f, 18f, 24f, 157f, 36100f, 8110f, 18.9f, 7.18f, 2550f, 541f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.3f, 0.000361f, 0.078f, 0.22f, 0.0718721440861576f));
        H_BEAM.Add(new Element("500mmX200mmX89.6kg", 89.6f, 500f, 200f, 10f, 16f, 20f, 114.2f, 47800f, 2140f, 20.5f, 4.33f, 1910f, 214f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.2f, 0.000478f, 0.052f, 0.25f, 0.0432886293277092f));
        H_BEAM.Add(new Element("488mmX300mmX128kg", 128f, 488f, 300f, 11f, 18f, 26f, 163.5f, 71000f, 8110f, 20.8f, 7.04f, 2910f, 541f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.3f, 0.00071f, 0.078f, 0.244f, 0.0704290043115622f));
        H_BEAM.Add(new Element("600mmX200mmX106kg", 106f, 600f, 200f, 11f, 17f, 22f, 134.4f, 77600f, 2280f, 24f, 4.12f, 2590f, 228f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.2f, 0.000776f, 0.052f, 0.3f, 0.0411877235523957f));
        H_BEAM.Add(new Element("588mmX300mmX151kg", 151f, 588f, 300f, 12f, 20f, 28f, 192.5f, 118000f, 9020f, 24.8f, 6.85f, 4020f, 601f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.3f, 0.00118f, 0.078f, 0.294f, 0.0684522774326339f));
        H_BEAM.Add(new Element("700mmX300mmX185kg", 185f, 700f, 300f, 13f, 24f, 28f, 235.5f, 201000f, 10800f, 29.3f, 6.78f, 5760f, 722f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.3f, 0.00201f, 0.078f, 0.35f, 0.067719917757972f));
        H_BEAM.Add(new Element("800mmX300mmX210kg", 210f, 800f, 300f, 14f, 26f, 28f, 267.4f, 292000f, 11700f, 33f, 6.62f, 7290f, 782f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.3f, 0.00292f, 0.078f, 0.4f, 0.0661473163814882f));
        H_BEAM.Add(new Element("900mmX300mmX243kg", 243f, 900f, 300f, 16f, 28f, 28f, 309.8f, 411000f, 12600f, 36.4f, 6.39f, 9140f, 843f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.3f, 0.00411f, 0.078f, 0.45f, 0.0637741333957655f));

    }

    void GenerateI()
    {
        Debug.Log("Gen I");
        I_BEAM = new List<Element>();
        I_BEAM.Add(new Element("150mmX75mmX17.1kg", 17.1f, 150f, 75f, 5.5f, 9.5f, 9f, 4.5f, 21.83f, 819f, 57.5f, 6.12f, 1.62f, 109f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.075f, 0.00000819f, 0.0195f, 0.075f, 0.0162295715350884f));
        I_BEAM.Add(new Element("200mmX100mmX26kg", 26f, 200f, 100f, 7f, 10f, 10f, 5f, 33.06f, 2170f, 138f, 8.11f, 2.05f, 217f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.1f, 0.0000217f, 0.026f, 0.1f, 0.0204309291886985f));
        I_BEAM.Add(new Element("200mmX150mmX50.4kg", 50.4f, 200f, 150f, 9f, 16f, 15f, 7.5f, 64.16f, 4460f, 753f, 8.34f, 3.43f, 446f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.15f, 0.0000446f, 0.039f, 0.1f, 0.0342582607399687f));
        I_BEAM.Add(new Element("250mmX125mmX38.3kg", 38.3f, 250f, 125f, 7.5f, 12.5f, 12f, 6f, 48.79f, 5180f, 337f, 10.3f, 2.63f, 414f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.125f, 0.0000518f, 0.0325f, 0.125f, 0.0262814632491125f));
        I_BEAM.Add(new Element("250mmX125mmX55.5kg", 55.5f, 250f, 125f, 10f, 19f, 21f, 10.5f, 70.73f, 7310f, 538f, 10.2f, 2.76f, 585f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.125f, 0.0000731f, 0.0325f, 0.125f, 0.0275796854570189f));
        I_BEAM.Add(new Element("300mmX150mmX48.3kg", 48.3f, 300f, 150f, 8f, 13f, 12f, 6f, 61.58f, 9480f, 588f, 12.4f, 3.09f, 632f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.15f, 0.0000948f, 0.039f, 0.15f, 0.030900735793117f));
        I_BEAM.Add(new Element("300mmX150mmX65.5kg", 65.5f, 300f, 150f, 10f, 18.5f, 19f, 9.5f, 83.47f, 12700f, 886f, 12.3f, 3.26f, 849f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.15f, 0.000127f, 0.039f, 0.15f, 0.0325800430770231f));
        I_BEAM.Add(new Element("300mmX150mmX76.8kg", 76.8f, 300f, 150f, 11.5f, 22f, 23f, 11.5f, 97.88f, 14700f, 1080f, 12.2f, 3.32f, 978f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.15f, 0.000147f, 0.039f, 0.15f, 0.0332173434888966f));
        I_BEAM.Add(new Element("350mmX150mmX58.5kg", 58.5f, 350f, 150f, 9f, 15f, 13f, 6.5f, 74.58f, 15200f, 702f, 14.3f, 3.07f, 870f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.15f, 0.000152f, 0.039f, 0.175f, 0.0306801420834759f));
        I_BEAM.Add(new Element("350mmX150mmX87.2kg", 87.2f, 350f, 150f, 12f, 24f, 25f, 12.5f, 111.1f, 22400f, 1180f, 14.2f, 3.26f, 1280f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.15f, 0.000224f, 0.039f, 0.175f, 0.0325899710128908f));
        I_BEAM.Add(new Element("400mmX150mmX72kg", 72f, 400f, 150f, 10f, 18f, 17f, 8.5f, 91.73f, 24100f, 864f, 16.2f, 3.07f, 1200f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.15f, 0.000241f, 0.039f, 0.2f, 0.0306903028812165f));
        I_BEAM.Add(new Element("40mmX150mmX95.8kg", 95.8f, 40f, 150f, 12.5f, 25f, 27f, 13.5f, 122.1f, 31700f, 1240f, 16.1f, 3.18f, 1580f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.15f, 0.000317f, 0.039f, 0.02f, 0.0318678680736728f));
        I_BEAM.Add(new Element("450mmX175mmX91.7kg", 91.7f, 450f, 175f, 11f, 20f, 19f, 9.5f, 116.8f, 39200f, 1510f, 18.3f, 3.6f, 1740f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.175f, 0.000392f, 0.0455f, 0.225f, 0.0359556423830542f));
        I_BEAM.Add(new Element("450mmX175mmX115kg", 115f, 450f, 175f, 13f, 26f, 27f, 13.5f, 146.1f, 48800f, 2020f, 18.3f, 3.72f, 2170f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.175f, 0.000488f, 0.0455f, 0.225f, 0.0371835265608537f));
        I_BEAM.Add(new Element("600mmX190mmX133kg", 133f, 600f, 190f, 13f, 25f, 25f, 12.5f, 169.4f, 98400f, 2460f, 24.1f, 3.81f, 3280f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.19f, 0.000984f, 0.0494f, 0.3f, 0.0381075344184966f));
        I_BEAM.Add(new Element("600mmX190mmX176kg", 176f, 600f, 190f, 16f, 35f, 38f, 19f, 224.5f, 130000f, 3540f, 24.1f, 3.97f, 4330f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.19f, 0.0013f, 0.0494f, 0.3f, 0.0397094121900724f));

    }

    public static void GenerateU()
    {
        U_PROP = new List<UElement>();
        string savedData = PlayerPrefs.GetString("UPROP");
        Debug.Log(savedData);
        if (savedData != "")
        {
            string[] elementStr = savedData.Split(null);
            foreach (string e in elementStr)
            {
                if (!string.IsNullOrEmpty(e))
                {
                    string[] eStr = e.Split(',');
                    float E = float.Parse(eStr[0]);
                    float I = float.Parse(eStr[1]);
                    U_PROP.Add(new UElement(E, I));
                }
            }
        }
    }

    public static void GeneratePIPE()
    {
        PIPE = new List<PElement>();
        PIPE.Add(new PElement("15 mm x 0.972 kg", 21.7f, 2f, 0.972f, 1.238f, 0.607f, 0.00700219213309808f, 0.56f, 14700000f, 24500000f, 1f));
        PIPE.Add(new PElement("20 mm x 1.41 kg", 27.2f, 2.3f, 1.41f, 1.799f, 1.41f, 0.00885307155976081f, 1.03f, 14700000f, 24500000f, 1f));
        PIPE.Add(new PElement("25 mm x 1.8 kg", 34f, 2.3f, 1.8f, 2.291f, 2.89f, 0.011231464190637f, 1.7f, 14700000f, 24500000f, 1f));
        PIPE.Add(new PElement("32 mm x 2.29 kg", 42.7f, 2.3f, 2.29f, 2.919f, 5.97f, 0.0143011222150021f, 2.8f, 14700000f, 24500000f, 1f));
        PIPE.Add(new PElement("40 mm x 2.63 kg", 48.6f, 2.3f, 2.63f, 3.345f, 8.99f, 0.0163938812458168f, 3.7f, 14700000f, 24500000f, 1f));
        PIPE.Add(new PElement("40 mm x 3.58 kg", 48.6f, 3.2f, 3.58f, 4.654f, 11.8f, 0.0159231070254589f, 4.86f, 14700000f, 24500000f, 1f));
        PIPE.Add(new PElement("50 mm x 4.52 kg", 60.5f, 3.2f, 4.52f, 5.76f, 23.7f, 0.0202844357410635f, 7.84f, 14700000f, 24500000f, 1f));
        PIPE.Add(new PElement("50 mm x 5.57 kg", 60.5f, 4f, 5.57f, 7.1f, 28.5f, 0.0200351803262218f, 9.41f, 14700000f, 24500000f, 1f));
        PIPE.Add(new PElement("65 mm x 5.77 kg", 76.3f, 3.2f, 5.77f, 7.349f, 49.2f, 0.0258742891817069f, 12.9f, 14700000f, 24500000f, 1f));
        PIPE.Add(new PElement("65 mm x 7.13 kg", 76.3f, 4f, 7.13f, 9.085f, 59.5f, 0.0255915162056903f, 15.6f, 14700000f, 24500000f, 1f));
        PIPE.Add(new PElement("80 mm x 6.78 kg", 89.1f, 3.2f, 6.78f, 8.636f, 79.8f, 0.0303980082719467f, 17.9f, 14700000f, 24500000f, 1f));
        PIPE.Add(new PElement("80 mm x 8.39 kg", 89.1f, 4f, 8.39f, 10.69f, 97f, 0.0301229162630518f, 21.8f, 14700000f, 24500000f, 1f));
        PIPE.Add(new PElement("90 mm x 7.76 kg", 101.6f, 3.2f, 7.76f, 9.892f, 120f, 0.0348296066035566f, 23.6f, 14700000f, 24500000f, 1f));
        PIPE.Add(new PElement("90 mm x 9.63 kg", 101.6f, 4f, 9.63f, 12.26f, 146f, 0.0345089061015597f, 28.8f, 14700000f, 24500000f, 1f));
        PIPE.Add(new PElement("100 mm x 8.77 kg", 114.3f, 3.2f, 8.77f, 11.17f, 172f, 0.0392407804977604f, 30.2f, 14700000f, 24500000f, 1f));
        PIPE.Add(new PElement("100 mm x 12.2 kg", 114.3f, 4.5f, 12.2f, 15.52f, 234f, 0.0388295243180094f, 41f, 14700000f, 24500000f, 1f));
        PIPE.Add(new PElement("100 mm x 15 kg", 114.3f, 5.6f, 15f, 19.12f, 283f, 0.0384723994964254f, 49.6f, 14700000f, 24500000f, 1f));
        PIPE.Add(new PElement("125 mm x 15 kg", 139.8f, 4.5f, 15f, 19.3f, 438f, 0.0476385353659563f, 62.7f, 14700000f, 24500000f, 1f));
        PIPE.Add(new PElement("125 mm x 19.8 kg", 139.8f, 6f, 19.8f, 25.22f, 566f, 0.0473735220852963f, 80.9f, 14700000f, 24500000f, 1f));
        PIPE.Add(new PElement("150 mm x 17.8 kg", 165.2f, 4.5f, 17.8f, 22.72f, 734f, 0.0568386646818598f, 88.9f, 14700000f, 24500000f, 1f));
        PIPE.Add(new PElement("150 mm x 23.6 kg", 165.2f, 6f, 23.6f, 30.01f, 952f, 0.0563229607534144f, 115f, 14700000f, 24500000f, 1f));
        PIPE.Add(new PElement("175 mm x 22.9 kg", 190.7f, 5f, 22.9f, 29.17f, 1260f, 0.0657229514106931f, 132f, 14700000f, 24500000f, 1f));
        PIPE.Add(new PElement("175 mm x 31.7 kg", 190.7f, 7f, 31.7f, 40.4f, 1710f, 0.06505899835785f, 179f, 14700000f, 24500000f, 1f));
        PIPE.Add(new PElement("200 mm x 31.1 kg", 216.3f, 6f, 31.1f, 39.61f, 2190f, 0.0743566193536705f, 203f, 14700000f, 24500000f, 1f));
        PIPE.Add(new PElement("200 mm x 41.1 kg", 216.3f, 8f, 41.1f, 52.35f, 2840f, 0.0736547614058042f, 263f, 14700000f, 24500000f, 1f));

    }

    public static void GenerateUT()
    {
        UT_PROP = new List<AElement>();
        string savedData = PlayerPrefs.GetString("UTPROP");
        Debug.Log(savedData);
        if (savedData != "")
        {
            string[] elementStr = savedData.Split(null);
            foreach (string e in elementStr)
            {
                if (!string.IsNullOrEmpty(e))
                {
                    string[] eStr = e.Split(',');
                    float E = float.Parse(eStr[0]);
                    float A = float.Parse(eStr[1]);
                    UT_PROP.Add(new AElement(E,A));
                }
            }
        }
    }
}
