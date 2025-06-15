# AR Reading Aid

This repository contains the source code and documentation for the AR Reading Aid application, developed as part of the KIT208 Virtual and Mixed Reality Technology unit at the University of Tasmania.

The app uses Vuforia in Unity to assist early readers by overlaying interactive digital content on real-world text, enhancing literacy and engagement through multimodal feedback.

---

## Features

- **Text Recognition**: Detects printed text using Vuforia's text recognition.
- **Interactive UI**: Allows users to manipulate text appearance (font, spacing, size, color).
- **Text-to-Speech**: Uses Google Cloud Text-to-Speech for real-time auditory feedback.
- **Word Analysis Tools**:
  - Syllable splitting
  - Dictionary lookup
  - Word highlighting
- **Camera Feed Display**: AR camera view with digital overlays.
- **Gesture-based Interactions**: Touch-based control for triggering features.

---

## Technical Stack

- **Unity** (2022.3.x)
- **Vuforia Engine**
- **Google Cloud TTS API**
- **TextMeshPro**
- **C# Scripts** (modularized for readability and scalability)

---

## Setup Instructions

1. Clone this repository to your local machine.
2. Open the project using Unity Hub (tested on Unity 2022.3 LTS).
3. Import the **Vuforia Engine** through the Unity Package Manager.
4. Insert your **Vuforia license key** in the ARCamera settings.
5. Configure your **Google Cloud API key** in the TextToSpeech script.
6. Build the project for Android (or compatible mobile platform).
7. Test using physical text printouts and your device's camera.

---

## Acknowledgements

- Unity Technologies
- Vuforia Engine
- Google Cloud Services
- TextMeshPro
- UTAS KIT208 Teaching Team

---

## License

All content in this repository is the intellectual property of Joshua Crisford and Athen Jumper and may not be used for commercial purposes. Redistribution or reuse without explicit permission is not allowed.
