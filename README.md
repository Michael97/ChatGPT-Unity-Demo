# ChatGPT-Unity-Demo: A hands-on exploration of OpenAI's ChatGPT model inside Unity as an AI Agent.

## :star2: Project Overview

This project, currently under active development, represents a hands-on exploration of Large Language Models (LLMs) in the gaming industry. By integrating the OpenAI API wrapper into a rudimentary 2D game, I've equipped an agent with the ability to understand, interact with, and navigate the gaming environment based on predefined actions. This approach underscores the potential of LLMs to augment gameplay and bolster player immersion.

While this project demonstrates the initial promise of LLMs in gaming, including enhancing game experiences, facilitating complex in-game situations, and potentially sparking innovation in game design, it's important to note that it is an ongoing work in progress. As a solo developer working on this project in my spare time, updates may be sporadic. Nonetheless, this project aims to serve as a practical example for other developers interested in harnessing the power of LLMs within their gaming projects, specifically within the Unity game engine.




## :rocket: Getting Started

1. **Clone the Repository**: Use `git clone` to get the project onto your local machine.

2. **Install Unity**: Make sure you have the correct version of Unity, as specified in `ProjectVersion.txt`.

3. **Open the Project**: Add the project to the Unity Hub and open it.

4. **Install Dependencies**: The OpenAI API wrapper is fetched automatically from `https://github.com/hexthedev/OpenAi-Api-Unity.git`.

5. **API Key**: Head over to the OpenAI API wrapper repo and follow the steps there.

6. **Run the Project**: Open the "SingleAgent" scene and press the `Play` button. You can add more agents, but costs will increase and functionality around this is still WiP.






## :computer: How to Use the Project

This section provides an overview of how to use the project, including instructions on adding new tiles, agents, and other customizations. Follow the sub-sections below to get started:

### Adding New Tiles

To add new tiles to the game, follow these steps:

1. Go to Assets/Prefabs/Tiles/Data
2. Right click -> Create -> TileData -> CustomTileDataSO
3. Implement the necessary functionality and behaviors for your custom tile.
4. Drag the BaseTile from Assets/Prefabs/Tiles/Default into the scene and unpack completely. 
5. Add your new data to the tile, and/or update the tile script to what you want your tile to be Assets/Scripts/Tiles.
6. Rename tile to your liking and make it a prefab to save it
7. Add this new prefab to the tile palette Assets/Palettes/WorldPalette.prefab
8. Test the new tile in the game to ensure it functions as expected.

![New Agent Example](docs/new_tile.gif)

### Adding New Agents

To introduce new agents or AI characters to the game, follow these steps:

1. Go to Assets/ScriptableObjects/Agents
2. Right click -> Create -> ChatGpt -> ChatGptAgentData
3. Update name and personailty (Possibly extended in the future).
4. Find GPTInitializer in the scene to add it to the scene.

![New Agent Example](docs/new_agent.gif)

## :bulb: Project Insights

This project began its journey at a time when the potential of large language models (LLMs) in gaming was just being realized. Since then, remarkable advancements in the field, such as Voyager, have greatly influenced our understanding of AI agents in game environments.

Voyager, an LLM-powered embodied lifelong learning agent in Minecraft, introduced a groundbreaking approach to AI agent implementation. Unlike my version, Voyager adopts a self-generated plan by leveraging its own capabilities. It autonomously creates plans and writes code based on those plans. In contrast, my current implementation lacks a specific plan and relies on a generic "survive" objective. As a result, the chat-based LLM can sometimes generate hallucinatory responses and fail to perform adequately.

Drawing inspiration from Voyager's findings, it is evident that improving the prompt and providing clearer world data can significantly enhance the LLM's performance. By incorporating Voyager's approach of enabling the LLM to develop its own plans, we can expect to witness substantial improvements in generating more accurate and contextually relevant responses.

While my implementation was on the right track, these insights from Voyager's research provide a clear direction for enhancing the prompt and refining the interactions between the AI agent and the game environment. By allowing the LLM to generate its own plans based on its knowledge and understanding of the game world, we can unlock the full potential of LLMs in this project. As time permits, I plan to incorporate these improvements to further showcase the capabilities of LLMs in gaming and demonstrate the significant enhancements they can bring to AI agent performance.

Click [here](https://voyager.minedojo.org/) to learn more about Voyager.

## :handshake: Contributions

Contributions to the project are always welcome. Whether you're improving the code, adding new features or providing bug fixes, your input is highly appreciated. Please fork the repository and create a pull request to suggest changes. Please bare in mind I am a solo developer creating this in my own time.

## :page_facing_up: License

This project is open source and available under the [MIT License](LICENSE).

## :trophy: Credits and Acknowledgements

I'm developing this project in my spare time, as a solo developer, keen to learn and explore the possibilities of LLM integration within game development.

I would like to acknowledge:

- [hexthedev](https://github.com/hexthedev) for the OpenAi-Api-Unity wrapper. This tool was instrumental in developing the project.

- [Kenny](https://www.kenney.nl/assets) for the 2d assets used.

- The OpenAI team for developing the GPT-3.5 model, providing an intriguing and complex AI to work with.

Please feel free to connect if you have any questions or comments. Your feedback is welcomed.

## :warning: Warnings
Using the OpenAI API, as demonstrated in this project, may incur charges. Please review OpenAI's pricing details to understand the potential costs. Monitor your API usage carefully to avoid unexpected expenses. Exercise caution with the frequency and scope of interactions, especially during development and testing. This project is for demonstration purposes, and I am not liable for any incurred costs.
