using System;

namespace BMICalcWinUI.Models
{
    public class BMI
    {
        // Fields
        private double _height;
        private double _weight;
        private double _bmi;
        private double _optimalWeight;

        // Properties
        public string Name { get; set; }
        public double Height
        {
            get { return _height; }
            set
            {
                if (value <= 0) return;
                _height = value;
            }
        }
        public double Weight
        {
            get { return _weight; }
            set
            {
                if (value <= 0) return;
                _weight = value;
            }
        }
        public double Bmi
        {
            get { return _bmi; }
        }
        public double OptimalWeight
        {
            get { return _optimalWeight; }
        }

        // Constructor
        private BMI(string name, double h, double w)
        {
            Name = name;
            Height = h;
            Weight = w;
        }

        // Methods
        public void SetHeightAndWeight(double h, double w)
        {
            Height = h;
            Weight = w;
        }
        public double CalcBMI()
        {
            _bmi = _weight / Math.Pow(2, _height);
            return _bmi;
        }
        public double CalcBMI(double h, double w)
        {
            Height = h;
            Weight = w;
            return CalcBMI();
        }
        public double CalcOptimalWeight()
        {
            _optimalWeight = Math.Pow(2, _height) * 22;
            return _optimalWeight;
        }
        public double CalcOptimalWeight(double h)
        {
            Height = h;
            return CalcOptimalWeight();
        }

        public static BMI Init(string name, double h, double w)
        {
            var bmiObj = new BMI(name, h, w);
            bmiObj.CalcBMI();
            bmiObj.CalcOptimalWeight();
            return bmiObj;
        }

        public static BMI Parse(string s)
        {
            var bmidata = s.Trim().Split(',');
            var name = bmidata[0].Trim('\"');
            var h = double.Parse(bmidata[1].Trim('\"'));
            var w = double.Parse(bmidata[2].Trim('\"'));
            var b = double.Parse(bmidata[3].Trim('\"'));
            var o = double.Parse(bmidata[4].Trim('\"'));
            var bmiObj = new BMI(name, h, w);
            bmiObj._bmi = b;
            bmiObj._optimalWeight = o;
            return bmiObj;
        }

        public override string ToString()
        {
            return string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\"\n", Name, _height, _weight, _bmi, _optimalWeight);
        }
    }
}