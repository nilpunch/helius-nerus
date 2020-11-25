using UnityEngine;

public static class RectTransformExtensions
{
	public static bool Overlaps(this RectTransform rectTrans1, RectTransform rectTrans2)
	{
		Rect rect1 = new Rect(rectTrans1.localPosition.x, rectTrans1.localPosition.y, rectTrans1.rect.width, rectTrans1.rect.height);
		Rect rect2 = new Rect(rectTrans2.localPosition.x, rectTrans2.localPosition.y, rectTrans2.rect.width, rectTrans2.rect.height);

		return rect1.Overlaps(rect2);
	}

	public static Rect GetHelathyRect(this RectTransform rectTransform)
	{
		return new Rect(rectTransform.position.x - rectTransform.rect.width / 2f, rectTransform.position.y - rectTransform.rect.height / 2f, rectTransform.rect.width, rectTransform.rect.height);
	}
}
