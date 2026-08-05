using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UntitledTools {
	namespace LandscapePopulator {

		//The landscape populator main class
		public class LandscapePopulatorMain : MonoBehaviour {

			//All settings for the landscape populator
			[Space(10f)]
			[Header("Populator General Settings")]
			[Space(10f)]
			[Tooltip("The terrain that the generation system uses")]
			public Terrain TerrainToUse;
			[Tooltip("The seed the the generation system uses")]
			public int Seed = 1234;
			[Tooltip("Generates a random seed every time you run the generator")]
			public bool RandomSeed = true;
			[Tooltip("The higher this number is, the more tests the generator will run, resulting in a better-looking result")]
			public int ObjectIterations = 20;
			[Tooltip("How close together the objects can be\n(lower is higher detail)")]
			public int Detail = 5;
			[Tooltip("Adds in some random transformation after generation for a more realistic look,\nAlso allows clusters to be created.")]
			public bool AntiGrid = false;
			[Tooltip("Uses a smooth map to place objects in a smart manner.")]
			public bool FeatureMapping = true;
			[Space(10f)]
			[Header("Feature Mapping Settings")]
			[Space(10f)]
			[Tooltip("The lacunarity for the perlin noise map that the feature mapper uses.")]
			public float Lacunarity = 2f;
			[Tooltip("The persistance for the perlin noise map that the feature mapper uses.")]
			public float Persistance = 0.5f;
			[Tooltip("The octave count for the perlin noise map that the feature mapper uses.")]
			public int Octaves = 3;
			[Tooltip("The scale for the perlin noise map that the feature mapper uses.")]
			public int Scale = 5;

			//All of the objects for the Landscape Populator to use
			[Space(7f)]
			[Header("Object Settings")]
			[Space(7f)]
			[Tooltip("All of the objects that the generator can generate\n(Note: if the unity terrain system does not support an object, it will not generate properly)")]
			public List<LandscaperObject> Objects = new List<LandscaperObject> ();

			//Stores the terrain data as a private object
			private TerrainData TData;
			[SerializeField]
			private Texture2D FeatureMap;

			//Generates all of the objects on the selected terrain
			public void GenerateObjects () {

				//If there is no terrain selected, it finds a terrain
				if (TerrainToUse == null) {
					print("Terrain Not Found, Finding A Terrain : Landscape Populator");
					TerrainToUse = Terrain.activeTerrain;
				}

				if (FeatureMapping) {
					FeatureMap = LandscapePopulatorAPIs.GetTextureFrom2DFloat 
						(LandscapePopulatorAPIs.GetFeatureMap 
							(TerrainToUse.terrainData.heightmapResolution, 
								TerrainToUse.terrainData.heightmapResolution, Lacunarity, Persistance, 
								((float)TerrainToUse.terrainData.heightmapResolution / (float)Scale), Seed, Octaves));
					FeatureMap.Apply ();
				}

				//If there are no objects, 
				if (Objects.Count < 1) {
					print("No Objects Set : Landscape Populator");
					return;
				}

				//Generates a random seed if "Random Seed" Is Selected
				if (RandomSeed) {
					Seed = Random.Range (int.MinValue, int.MaxValue);
				}

				//Creates the terrain tree prototypes
				TData = TerrainToUse.terrainData;
				TreePrototype[] ObjectsToPlace = new TreePrototype[Objects.Count];
				for (int i = 0; i < Objects.Count; i++) {
					ObjectsToPlace [i] = new TreePrototype ();
					ObjectsToPlace [i].prefab = Objects [i].Prefab;
				}

				//Sets the prototypes and find the width and height of the terrain
				TData.treePrototypes = ObjectsToPlace;
				int Width = (int)TData.size.x;
				int Height = (int)TData.size.z;

				//Finds the progress bar's increase amount along with some other things
				float ProgressBarAdd = 1f / ((TData.size.x / (float)Detail * TData.size.z / (float)Detail));
				int TotalOperations = (Width / Detail) * (Height / Detail);
				System.Random PRNGenerator = new System.Random (Seed);
				int ProgressTracker = 0;
				for (int y = 0; y < Height; y += Detail) {
					for (int x = 0; x < Width; x += Detail) {

						bool CanCreateInstance = false;

						//Finds and sets some of the object's settings
						Vector3 ObjectPosition = new Vector3 ((float)x, 0f, (float)y);
						float SampledHeight = TerrainToUse.SampleHeight (ObjectPosition);
						float DensityThreasholdValue = (float)PRNGenerator.NextDouble ();
						float NormalizedX = (float)x / TData.size.x;
						float NormalizedZ = (float)y / TData.size.z;
						float SlopeAtPosition = TData.GetSteepness (NormalizedX, NormalizedZ);
						float NormalizedSlopeAtPos = Mathf.InverseLerp (0f, 90f, SlopeAtPosition);
						float FeatureMapAtPoint = -1f;
						if (FeatureMapping) {
							FeatureMapAtPoint = FeatureMap.GetPixel (x, y).grayscale;
						}

						//Runs all of the object iterations
						int ObjIndex = 0;
						for (int i = 0; i < ObjectIterations; i++) {
							ObjIndex = PRNGenerator.Next (0, Objects.Count);
							if (Objects [ObjIndex].MinHeight <= SampledHeight 
								&& Objects [ObjIndex].MaxHeight >= SampledHeight 
								&& Objects [ObjIndex].ObjectDensity >= DensityThreasholdValue 
								&& Objects [ObjIndex].MaximumSlope >= NormalizedSlopeAtPos 
								&& Objects [ObjIndex].FeatureDensity >= FeatureMapAtPoint) {

								CanCreateInstance = true;
								break;

							} else {
								CanCreateInstance = false;
							}
						}

						//Runs only if it is allowed to create an instance of an object
						if (CanCreateInstance) {

							Vector3 OriginalPos = ObjectPosition;

							//If anti-grid is on
							if (AntiGrid) {
								
								ObjectPosition += new Vector3 (Random.Range (-(float)Detail, (float)Detail), 0f, Random.Range (-(float)Detail, (float)Detail));
								if (Objects [ObjIndex].ClusterCount > 0) {
									
									for (int c = 0; c < Objects [ObjIndex].ClusterCount; c++) {

										//Generates a new object with cluster spawning enabled
										TreeInstance NewObject = new TreeInstance ();
										NewObject.prototypeIndex = ObjIndex;
										NewObject.position = new Vector3 (Mathf.InverseLerp (0f, TData.size.x, ObjectPosition.x), 
											TerrainToUse.SampleHeight (ObjectPosition), 
											Mathf.InverseLerp (0f, TData.size.z, ObjectPosition.z)) + TerrainToUse.GetPosition ();
										NewObject.heightScale = 1f;
										NewObject.widthScale = 1f;
										TerrainToUse.AddTreeInstance (NewObject);
										TerrainToUse.Flush ();

										//Anti-grids the object
										ObjectPosition = OriginalPos;
										ObjectPosition += new Vector3 (Random.Range (-(float)Detail, (float)Detail), 0f, Random.Range (-(float)Detail, (float)Detail));

									}

								} else {

									//Generates a new object
									TreeInstance NewObject = new TreeInstance ();
									NewObject.prototypeIndex = ObjIndex;
									NewObject.position = new Vector3 (Mathf.InverseLerp (0f, TData.size.x, ObjectPosition.x), 0f, Mathf.InverseLerp (0f, TData.size.z, ObjectPosition.z)) + TerrainToUse.GetPosition ();
									NewObject.heightScale = 1f;
									NewObject.widthScale = 1f;
									TerrainToUse.AddTreeInstance (NewObject);
									TerrainToUse.Flush ();

								}

							} else {

								//Generates a new object
								TreeInstance NewObject = new TreeInstance ();
								NewObject.prototypeIndex = ObjIndex;
								NewObject.position = new Vector3 (Mathf.InverseLerp (0f, TData.size.x, ObjectPosition.x), 0f, Mathf.InverseLerp (0f, TData.size.z, ObjectPosition.z)) + TerrainToUse.GetPosition ();
								NewObject.heightScale = 1f;
								NewObject.widthScale = 1f;
								TerrainToUse.AddTreeInstance (NewObject);
								TerrainToUse.Flush ();

							}

						}

					}
				}

				//Updates the terrain
				TerrainToUse.Flush ();

			}

			//Clears all terrain objects
			public void WipeAllObjects () {

				if (EditorUtility.DisplayDialog ("Confirmation Menu", "Are you sure you would like to delete all terrain objects?", "Yes", "No")) {
					StopAllCoroutines ();
					EditorUtility.ClearProgressBar ();

					TData = TerrainToUse.terrainData;
					TData.treeInstances = new TreeInstance[0];
					TData.treePrototypes = new TreePrototype[0];
					TerrainToUse.Flush ();
				}

			}

		}


		//The object class that the Landscape Populator uses
		[System.Serializable]
		public class LandscaperObject {

			//All of the stored settings per Landscaper Object
			[Tooltip("What object will use these settings and be used by the generator")]
			public GameObject Prefab;
			[Range(0f, 1f)]
			[Tooltip("How much of the object will be")]
			public float ObjectDensity = 0.5f;
			[Range(0f, 1f)]
			[Tooltip("The maximum slope of the terrain that the object can spawn on\n(Note: normalized from  zero to ninty  to  zero to one)")]
			public float MaximumSlope = 1f;
			[Tooltip("The minimum height at which the object will be able to spawn")]
			public float MinHeight = 0f;
			[Tooltip("The maximum height at which the object will be able to spawn")]
			public float MaxHeight = 1000f;
			[Range(1, 100)]
			[Tooltip("If anti-grid is on, you can set an object to generate multiple of itself around itself\n(takes longer to generate)")]
			public int ClusterCount = 0;
			[Range(0f, 1f)]
			[Tooltip("The density of the object used by the feature mapper.")]
			public float FeatureDensity = 0.5f;

			//Creates a new Landscaper Object
			public LandscaperObject (GameObject ObjectVal, float SlopeVal = 1f, float MinHeightVal = 0f, float MaxHeightVal = 1000f, float Density = 0.5f, float _FeatureDensity = 0.5f) {
				Prefab = ObjectVal;
				MaximumSlope = SlopeVal;
				MinHeight = MinHeightVal;
				MaxHeight = MaxHeightVal;
				ObjectDensity = Density;
				FeatureDensity = _FeatureDensity;
			}

		}

	}
}


