using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum ItemType
{
	Quest,Equip,Consume
}
public class ItemSlot : MonoBehaviour {
	Image itemImage;
	Equip equip;
	public void selected()
	{
		//show description
	}
	public void wear()
	{
		itemImage.SetNativeSize();
		remove();
	}

	public void use()
	{
		remove();
	}

	public void sell()
	{
		remove();
	}
	public void remove()
	{
		itemImage = null;
	}
}
