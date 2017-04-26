using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementStore : MonoBehaviour {

    public List<Element> H_BEAM,I_BEAM;
    public struct Element
    {
        public string name;
        public float w,h,b,t1,t2,r,area,ix,iy,rx,ry,zx,zy,k,e,fb,cb,fy,bf;
        public Element(string name,float w,float h,float b,float t1,float t2,float r,float area,float ix,float iy,float rx,float ry,float zx,float zy,float k,float e,float fb,float cb,float fy,float bf)
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
        }
    }

    // Use this for initialization
    void Start () {
        GenerateH();
        GenerateI();
    }

    void GenerateH()
    {
        H_BEAM = new List<Element>();
        H_BEAM.Add(new Element("100x50", 9.3f, 100f, 50f, 5f, 7f, 8f, 11.85f, 187f, 14.8f, 3.98f, 1.12f, 37.5f, 5.91f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.05f));
        H_BEAM.Add(new Element("100x100", 17.2f, 100f, 100f, 6f, 8f, 10f, 21.9f, 383f, 134f, 4.18f, 2.47f, 76.5f, 26.7f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.1f));
        H_BEAM.Add(new Element("125x125", 23.8f, 125f, 125f, 6.5f, 9f, 10f, 30.31f, 847f, 293f, 5.29f, 3.11f, 136f, 47f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.125f));
        H_BEAM.Add(new Element("150x75", 14f, 150f, 75f, 5f, 7f, 8f, 17.85f, 666f, 49.5f, 6.11f, 1.66f, 88.8f, 13.2f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.075f));
        H_BEAM.Add(new Element("150x100", 21.1f, 148f, 100f, 6f, 9f, 11f, 26.84f, 1020f, 151f, 6.17f, 2.37f, 138f, 30.1f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.1f));
        H_BEAM.Add(new Element("150x150", 31.5f, 150f, 150f, 7f, 10f, 11f, 40.14f, 1640f, 563f, 6.39f, 3.75f, 219f, 75.1f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.15f));
        H_BEAM.Add(new Element("175x175", 40.2f, 175f, 175f, 7.5f, 11f, 12f, 51.21f, 2880f, 984f, 7.5f, 4.38f, 330f, 112f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.175f));
        H_BEAM.Add(new Element("200x100", 21.3f, 200f, 100f, 5.5f, 8f, 11f, 27.16f, 1840f, 134f, 8.24f, 2.22f, 184f, 26.8f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.1f));
        H_BEAM.Add(new Element("200x150", 30.6f, 194f, 150f, 6f, 9f, 13f, 39.01f, 2690f, 507f, 8.3f, 3.61f, 277f, 67.6f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.15f));
        H_BEAM.Add(new Element("200x200", 49.9f, 200f, 200f, 8f, 12f, 13f, 63.53f, 4720f, 1600f, 8.62f, 5.02f, 472f, 160f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.2f));
        H_BEAM.Add(new Element("250x125", 39.6f, 250f, 125f, 6f, 9f, 12f, 37.66f, 4050f, 294f, 10.4f, 2.79f, 324f, 47f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.125f));
        H_BEAM.Add(new Element("250x175", 44.1f, 244f, 175f, 7f, 11f, 16f, 56.24f, 6120f, 984f, 10.4f, 4.18f, 502f, 113f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.175f));
        H_BEAM.Add(new Element("250x250", 72.4f, 250f, 250f, 9f, 14f, 16f, 92.18f, 10800f, 3650f, 10.8f, 6.29f, 867f, 292f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.25f));
        H_BEAM.Add(new Element("300x150", 36.7f, 300f, 150f, 6.5f, 9f, 13f, 46.78f, 7210f, 508f, 12.4f, 3.29f, 481f, 67.7f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.15f));
        H_BEAM.Add(new Element("300x200", 56.8f, 294f, 200f, 8f, 12f, 18f, 72.38f, 1130f, 1600f, 12.5f, 4.71f, 771f, 160f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.2f));
        H_BEAM.Add(new Element("300x300", 94f, 300f, 300f, 10f, 15f, 18f, 119.8f, 20400f, 6750f, 13.1f, 7.51f, 1360f, 450f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.3f));
        H_BEAM.Add(new Element("350x175", 49.6f, 350f, 175f, 7f, 11f, 14f, 63.14f, 13600f, 984f, 14.7f, 3.95f, 775f, 112f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.175f));
        H_BEAM.Add(new Element("350x250", 79.7f, 340f, 250f, 9f, 14f, 20f, 101.5f, 21700f, 3650f, 14.6f, 6f, 1280f, 292f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.25f));
        H_BEAM.Add(new Element("350x350", 137f, 350f, 350f, 12f, 19f, 20f, 173.9f, 40300f, 13600f, 15.2f, 8.84f, 2300f, 776f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.35f));
        H_BEAM.Add(new Element("400x200", 66f, 400f, 200f, 8f, 13f, 16f, 84.12f, 23700f, 1740f, 16.8f, 4.54f, 1190f, 174f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.2f));
        H_BEAM.Add(new Element("400x300", 107f, 390f, 300f, 10f, 16f, 22f, 136f, 38700f, 7210f, 16.9f, 7.28f, 1980f, 481f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.3f));
        H_BEAM.Add(new Element("400x400", 172f, 400f, 400f, 13f, 21f, 22f, 218.7f, 66600f, 22400f, 17.5f, 10.1f, 3330f, 1120f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.4f));
        H_BEAM.Add(new Element("450x200", 76f, 450f, 200f, 9f, 14f, 18f, 69.76f, 33500f, 1870f, 16.8f, 4.4f, 1490f, 187f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.2f));
        H_BEAM.Add(new Element("450x300", 124f, 440f, 300f, 11f, 18f, 24f, 157f, 36100f, 8110f, 18.9f, 7.18f, 2550f, 541f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.3f));
        H_BEAM.Add(new Element("500x200", 89.6f, 500f, 200f, 10f, 16f, 20f, 114.2f, 47800f, 2140f, 20.5f, 4.33f, 1910f, 214f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.2f));
        H_BEAM.Add(new Element("500x300", 128f, 488f, 300f, 11f, 18f, 26f, 163.5f, 71000f, 8110f, 20.8f, 7.04f, 2910f, 541f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.3f));
        H_BEAM.Add(new Element("600x200", 106f, 600f, 200f, 11f, 17f, 22f, 134.4f, 77600f, 2280f, 24f, 4.12f, 2590f, 228f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.2f));
        H_BEAM.Add(new Element("600x300", 151f, 588f, 300f, 12f, 20f, 28f, 192.5f, 118000f, 9020f, 24.8f, 6.85f, 4020f, 601f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.3f));
        H_BEAM.Add(new Element("700x300", 185f, 700f, 300f, 13f, 24f, 28f, 235.5f, 201000f, 10800f, 29.3f, 6.78f, 5760f, 722f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.3f));
        H_BEAM.Add(new Element("800x300", 210f, 800f, 300f, 14f, 26f, 28f, 267.4f, 292000f, 11700f, 33f, 6.62f, 7290f, 782f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.3f));
        H_BEAM.Add(new Element("900x300", 243f, 900f, 300f, 16f, 28f, 28f, 309.8f, 411000f, 12600f, 36.4f, 6.39f, 9140f, 843f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.3f));
    }

    void GenerateI()
    {
        I_BEAM = new List<Element>();
        I_BEAM.Add(new Element("150x75", 17.1f, 150f, 75f, 5.5f, 9.5f, 9f, 4.5f, 21.83f, 819f, 57.5f, 6.12f, 1.62f, 109f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.075f));
        I_BEAM.Add(new Element("200x100", 26f, 200f, 100f, 7f, 10f, 10f, 5f, 33.06f, 2170f, 138f, 8.11f, 2.05f, 217f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.1f));
        I_BEAM.Add(new Element("200x150", 50.4f, 200f, 150f, 9f, 16f, 15f, 7.5f, 64.16f, 4460f, 753f, 8.34f, 3.43f, 446f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.15f));
        I_BEAM.Add(new Element("250x125", 38.3f, 250f, 125f, 7.5f, 12.5f, 12f, 6f, 48.79f, 5180f, 337f, 10.3f, 2.63f, 414f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.125f));
        I_BEAM.Add(new Element("250x125", 55.5f, 250f, 125f, 10f, 19f, 21f, 10.5f, 70.73f, 7310f, 538f, 10.2f, 2.76f, 585f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.125f));
        I_BEAM.Add(new Element("300x150", 48.3f, 300f, 150f, 8f, 13f, 12f, 6f, 61.58f, 9480f, 588f, 12.4f, 3.09f, 632f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.15f));
        I_BEAM.Add(new Element("300x150", 65.5f, 300f, 150f, 10f, 18.5f, 19f, 9.5f, 83.47f, 12700f, 886f, 12.3f, 3.26f, 849f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.15f));
        I_BEAM.Add(new Element("300x150", 76.8f, 300f, 150f, 11.5f, 22f, 23f, 11.5f, 97.88f, 14700f, 1080f, 12.2f, 3.32f, 978f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.15f));
        I_BEAM.Add(new Element("350x150", 58.5f, 350f, 150f, 9f, 15f, 13f, 6.5f, 74.58f, 15200f, 702f, 14.3f, 3.07f, 870f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.15f));
        I_BEAM.Add(new Element("350x150", 87.2f, 350f, 150f, 12f, 24f, 25f, 12.5f, 111.1f, 22400f, 1180f, 14.2f, 3.26f, 1280f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.15f));
        I_BEAM.Add(new Element("400x150", 72f, 400f, 150f, 10f, 18f, 17f, 8.5f, 91.73f, 24100f, 864f, 16.2f, 3.07f, 1200f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.15f));
        I_BEAM.Add(new Element("400x150", 95.8f, 40f, 150f, 12.5f, 25f, 27f, 13.5f, 122.1f, 31700f, 1240f, 16.1f, 3.18f, 1580f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.15f));
        I_BEAM.Add(new Element("450x175", 91.7f, 450f, 175f, 11f, 20f, 19f, 9.5f, 116.8f, 39200f, 1510f, 18.3f, 3.6f, 1740f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.175f));
        I_BEAM.Add(new Element("450x175", 115f, 450f, 175f, 13f, 26f, 27f, 13.5f, 146.1f, 48800f, 2020f, 18.3f, 3.72f, 2170f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.175f));
        I_BEAM.Add(new Element("600x190", 133f, 600f, 190f, 13f, 25f, 25f, 12.5f, 169.4f, 98400f, 2460f, 24.1f, 3.81f, 3280f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.19f));
        I_BEAM.Add(new Element("600x190", 176f, 600f, 190f, 16f, 35f, 38f, 19f, 224.5f, 130000f, 3540f, 24.1f, 3.97f, 4330f, 1f, 20400000000f, 14700000f, 1f, 24500000f, 0.19f));
    }
}
