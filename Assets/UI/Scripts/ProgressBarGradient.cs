using UnityEngine;
using System.Collections;

public class ProgressBarGradient : ProgressBar {
	public Gradient gradient;

	new protected void Update () {
		base.Update();
		fillColor = gradient.Evaluate(MathExt.Ratio(value,minValue,maxValue));
	}
}
