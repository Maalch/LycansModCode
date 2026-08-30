using System.Collections.Generic;
using UnityEngine;

namespace LycansNewRoles;

public class CharacterSkin
{
	public GameObject Metarig;

	public GameObject SkinMeshRenderer;

	public Shader DefaultShader;

	public Avatar Avatar;

	public bool IsOriginalSkin;

	public List<Texture> SkinTextures = new List<Texture>();

	public Texture SkinPetrified;

	public Texture SkinZombified;

	public Texture SkinPoisoned;

	public List<Texture> TopTextures = new List<Texture>();
}
