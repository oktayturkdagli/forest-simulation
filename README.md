# 📋 Simple Forest Simulation

Simple Forest Simulator is a basic forest simulation developed using Unity. This project demonstrates Unity's AI Navigation, character control, and automatic scene generation capabilities to create a dynamic forest environment.

- **Grid-Based Scene Generation**: Utilizes a grid system to create the gameplay area.
- **Random Object Placement**: Trees, rocks, and animals are randomly placed in the scene.
- **AI Animal Movements**: Animals wander around the scene using NavMesh AI.
- **Character Control**: Players can control a character in a third-person perspective.

## How to Use
The project comes ready to run or build. It can be experienced by running it directly.
Once the game starts, you can move the character using the WASD keys and control the camera angle with the mouse. Explore the randomly generated forest and observe the animals.

You can recreate the forest randomly before starting the game. 
To do so, click on the `Create Randomize Forest` button in the `GameManager` game object in the hierarchy. This will generate a new forest with new trees, rocks, and animals.

Also, to recalculate the Occlusion Culling map, go to `Window > Rendering > Occlusion Culling` to open the `Occlusion Culling window` and click the `Bake` button to create the Occlusion Culling map. Unity will calculate and render the Occlusion Culling map for the scene.

## Development

This project was developed using Unity 2022.3 LTS. Key features and concepts used include:

- The Character Controller system was used to generate character movements.
- NavMesh AI system was used to generate animal movements.
- Grid system was developed to create grid-based environment layout.
- LOD system was used to dynamically load objects around the user based on distance and camera.
- Occlusion Culling system was used to hide objects not visible to the camera.

# Visuals

https://github.com/oktayturkdagli/forest-simulation/assets/61520877/6e6c0254-659f-450b-ac15-6dea47c1822b
