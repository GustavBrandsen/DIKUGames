using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System;
using DIKUArcade.Events;
using DIKUArcade.Input;

namespace Breakout {
    public class Level : ILevel {
        public EntityContainer<Block> Blocks {private set; get;} = new EntityContainer<Block>();
        public List<string> map { private set; get;} = new List<string>{};
        public Dictionary<string, string> meta { private set; get;} = new Dictionary<string, string>();
        public Dictionary<char, string> legend { private set; get;} = new Dictionary<char, string>();
        private string[] lines;
        public Level(string filename) {
            lines = System.IO.File.ReadAllLines(filename);
            SortData();
            CreateMap();
        }
		
        public void Render() {
            Blocks.RenderEntities();
        }

        // Read the strings from the txt file in "lines" and sort them into the lists/dictionaries
        private void SortData() {
			for (int i = 0; i < lines.Length - 1; i++) {
				int k = i;
				switch (lines[i]) {
					case "Map:":
						while (!(lines[k+1] == "Map/")) {
							k++;
							i++;
							map.Add(lines[k]);
						}
						break;
					case "Meta:":
						while (!(lines[k+1] == "Meta/")) {
							k++;
							i++;
							string name = lines[k].Split(":")[0];
							string val = lines[k].Split(":")[1].Remove(0,1);
							meta.Add(name, val);
						}
						break;
					case "Legend:":
						while (!(lines[k+1] == "Legend/")) {
							k++;
							i++;
							string img = lines[k].Split(" ")[1];
							legend.Add(lines[k][0], img);
						}
						break;
				}
			}
        }

        // Create the blocks in the EntityContainer
        private void CreateMap() {
			for (int y = 0; y < map.Count - 1; y++) {
				for (int x = 0; x < map[y].Length; x++) {
					if (meta.ContainsKey("Unbreakable") && map[y][x] == char.Parse(meta["Unbreakable"])) {
						Blocks.AddEntity(new UnbreakableBlock(
							new DynamicShape(new Vec2F(x/12f, 1f-(y/25f)), new Vec2F(1/12f, 0.04f)),
							new Image(Path.Combine("Assets", "Images", legend[map[y][x]])),
							(meta.ContainsKey("PowerUp") && char.Parse(meta["PowerUp"]) == map[y][x])
						));
					}
					if (meta.ContainsKey("Hardened") && map[y][x] == char.Parse(meta["Hardened"])) {
						Blocks.AddEntity(new HardenedBlock(
							new DynamicShape(new Vec2F(x/12f, 1f-(y/25f)), new Vec2F(1/12f, 0.04f)),
							new Image(Path.Combine("Assets", "Images", legend[map[y][x]])),
							(meta.ContainsKey("PowerUp") && char.Parse(meta["PowerUp"]) == map[y][x])
						));
					}
					else if (!(map[y][x] == '-')) {
						Blocks.AddEntity(new DefaultBlock(
							new DynamicShape(new Vec2F(x/12f, 1f-(y/25f)), new Vec2F(1/12f, 0.04f)),
							new Image(Path.Combine("Assets", "Images", legend[map[y][x]])),
							(meta.ContainsKey("PowerUp") && char.Parse(meta["PowerUp"]) == map[y][x])
						));
					}
				}
			}
        }


    }
}




