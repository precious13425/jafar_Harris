using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UntitledTools {
	namespace LandscapePopulator {

		public static class LandscapePopulatorAPIs {

			/// <summary>
			/// Gets the feature map.
			/// </summary>
			/// <returns>The height map.</returns>
			/// <param name="Width">Width.</param>
			/// <param name="Height">Height.</param>
			/// <param name="Lacunarity">Lacunarity.</param>
			/// <param name="Persistance">Persistance.</param>
			/// <param name="Scale">Scale.</param>
			/// <param name="Offset">Offset.</param>
			/// <param name="Seed">Seed.</param>
			/// <param name="Octaves">Octaves.</param>
			public static float[,] GetFeatureMap (int Width, int Height, float Lacunarity, float Persistance, float Scale, int Seed, int Octaves) {

				float[,] NoiseMapFloat = new float[Width, Height];

				System.Random PRNG = new System.Random (Seed);
				Vector2[] OctaveOffsets = new Vector2[Octaves];
				for (int o = 0; o < OctaveOffsets.Length; o++) {
					float OffsetX = (float)PRNG.Next (-100000, 100000);
					float OffsetY = (float)PRNG.Next (-100000, 100000);
					OctaveOffsets [o] = new Vector2 (OffsetX, OffsetY);
				}

				Vector2 MiddleOfMap = new Vector2 ((float)Width / 2, (float)Height / 2);
				float IslandifyEffect = (float)Width / 2;

				float MaxHeight = float.MinValue;
				float MinHeight = float.MaxValue;

				if (Scale <= 0f) {
					Scale = 0.0001f;
				}

				int EditorIndex = 0;

				//Creates the perlin map
				for (int x = 0; x < Width; x++) {
					for (int y = 0; y < Height; y++) {

						float NoiseMapVal = 0f;
						float Amplitude = 1f;
						float Frequency = 1f;

						for (int o = 0; o < Octaves; o++) {

							float SampledX = x / Scale * Frequency + OctaveOffsets[o].x;
							float SampledY = y / Scale * Frequency + OctaveOffsets[o].y;

							float PerlinMap = Mathf.PerlinNoise (SampledX, SampledY) * 2 - 1;
							NoiseMapVal += PerlinMap * Amplitude;
							NoiseMapFloat [x, y] = NoiseMapVal;

							Amplitude *= Persistance;
							Frequency *= Lacunarity;

						}

						if (NoiseMapVal > MaxHeight) {
							MaxHeight = NoiseMapVal;
						} else if (NoiseMapVal < MinHeight) {
							MinHeight = NoiseMapVal;
						}

					}
				}

				for (int x = 0; x < Width; x++) {
					for (int y = 0; y < Height; y++) {
						NoiseMapFloat [x, y] = Mathf.InverseLerp (MinHeight, MaxHeight, NoiseMapFloat [x, y]);
					}
				}

				return NoiseMapFloat;

			}

			/// <summary>
			/// Gets the texture from a 2D float array.
			/// </summary>
			/// <returns>The texture from a 2D float array.</returns>
			/// <param name="Values">Values.</param>
			public static Texture2D GetTextureFrom2DFloat (float[,] Values) {

				int Width = Values.GetLength (0);
				int Height = Values.GetLength (1);
				Texture2D ReturnTex = new Texture2D (Width, Height);

				for (int x = 0; x < Width; x++) {
					for (int y = 0; y < Height; y++) {
						float Value = Values [x, y];
						Color PixelColor = new Color (Value, Value, Value, Value);
						ReturnTex.SetPixel (x, y, PixelColor);
					}
				}

				ReturnTex.Apply ();
				return ReturnTex;

			}

		}

	}
}


