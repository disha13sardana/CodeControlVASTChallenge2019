# Code Control VAST Challenge 2019

**IEEE VAST Challenge 2019 Dataset in an Immersive Mixed Reality Environment**

An innovative mixed reality visualization system built with Unity and Microsoft HoloLens 2 that enables embodied interaction with the IEEE VAST Challenge 2019 earthquake disaster response dataset through spatial computing and multimodal interaction.

---

## 📋 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [System Requirements](#system-requirements)
- [Installation](#installation)
- [Dataset](#dataset)
- [Usage Guide](#usage-guide)
- [Interaction Methods](#interaction-methods)
- [Project Structure](#project-structure)
- [Architecture](#architecture)
- [Research Background](#research-background)
- [Troubleshooting](#troubleshooting)
- [Contributing](#contributing)
- [Publications](#publications)
- [License](#license)
- [Contact](#contact)

---

## 🎯 Overview

This project implements a **human-centered approach to visual analytics** by leveraging embodied interaction in mixed reality. Built for the Microsoft HoloLens 2, it transforms the IEEE VAST Challenge 2019 earthquake disaster response dataset into an immersive, spatial visualization that allows users to:

- **Walk around data** in physical space
- **Manipulate visualizations** using hand gestures
- **Control views** through voice commands
- **Navigate through time** using a slicing plane interface
- **Experience spatial audio** mapped to data attributes

Unlike traditional desktop-based analytics, this system uses the human body and multiple senses to interact with complex spatio-temporal data in novel and intuitive ways.

### About the IEEE VAST Challenge 2019

The Visual Analytics Science and Technology (VAST) Challenge 2019 focused on the fictional earthquake scenario **"Disaster at St. Himark"**, involving:

- **Mini-Challenge 1**: Crowdsourced earthquake damage reports from citizen scientists
- **Mini-Challenge 2**: Radiation sensor data (static and mobile sensors)
- **Mini-Challenge 3**: Social media analysis for emergency response

This MR system focuses primarily on **Mini-Challenge 1**, visualizing spatial, temporal, and categorical disaster data.

---

## ✨ Features

### Core Visualization Capabilities

- **3D Spatial Representation**: Geographic layout of St. Himark neighborhoods in physical space
- **Multiple Coordinated Views**: Linked 2D and 3D visualizations leveraging surrounding space
- **Temporal Navigation**: Time-based slicing plane for exploring data progression
- **Dynamic Data Display**: Video-like visual analytics with user-controlled playback

### Interaction Modalities

- **Hand Gesture Control**: 
  - Resize, rotate, and reposition visualization views
  - Navigate along the time axis using the slicing plane
  - Direct manipulation of 3D objects
  
- **Voice Commands**:
  - Show/hide different plot types (bar charts, histograms, scatterplots)
  - Switch between data categories
  - Control time progression
  
- **Spatial Audio**:
  - Auditory icons sonifying data attributes
  - Directional sound for spatial awareness
  - Aggregated data sonification

### Data Visualization Types

- **2D Visualizations**:
  - Bar charts
  - 2D histograms
  - Scatterplots
  - Time series plots
  
- **3D Visualizations**:
  - Map-plot (spatial time series)
  - 3D geographical representations
  - Volumetric data displays

### Data Categories

- Shake intensity (1-10 scale)
- Sewer and water infrastructure damage
- Power system status
- Roads and bridges condition
- Medical facility status
- Building damage levels
- Radiation measurements (static and mobile sensors)

---

## 🛠️ Technology Stack

### Primary Technologies

- **Platform**: Microsoft HoloLens 2
- **Engine**: Unity 2019.x or later
- **Development Language**: C# (97.9%)
- **Mixed Reality Framework**: Mixed Reality Toolkit (MRTK)

### Key Libraries & APIs

- **Microsoft Mixed Reality Toolkit (MRTK)**: Core MR functionality
- **Web Speech API**: Voice command integration
- **WebGL**: Browser-based deployment for 2D comparative studies
- **Unity Input System**: Gesture and interaction handling

---

## 💻 System Requirements

### For Development

#### Hardware
- **Development PC**:
  - Windows 10 (version 1903 or higher)
  - 64-bit processor
  - 16+ GB RAM
  - DirectX 11 compatible GPU
  - USB 3.0 port
  
- **Microsoft HoloLens 2** device

#### Software
- **Unity 2019.4 LTS** or later (recommended: Unity 2020.3 LTS)
- **Visual Studio 2019** or later with:
  - Universal Windows Platform development workload
  - Game development with Unity workload
  - Windows 10 SDK (10.0.18362.0 or later)
- **Mixed Reality Toolkit (MRTK) 2.7+**
- **Windows Device Portal** (for deployment)

### For Deployment/Usage

- **Microsoft HoloLens 2** with Windows Holographic OS
- **Optional**: Desktop PC with WebGL-compatible browser for 2D comparative analysis

---

## 📦 Installation

### Step 1: Clone the Repository

```bash
git clone https://github.com/disha13sardana/CodeControlVASTChallenge2019.git
cd CodeControlVASTChallenge2019
```

### Step 2: Install Unity and Required Tools

1. **Install Unity Hub** from [https://unity.com/download](https://unity.com/download)
2. **Install Unity 2019.4 LTS** or **Unity 2020.3 LTS** through Unity Hub
3. During installation, include:
   - Universal Windows Platform Build Support
   - Windows Build Support (IL2CPP)

### Step 3: Install Visual Studio

1. Download **Visual Studio 2019 Community** or later
2. During installation, select:
   - **Universal Windows Platform development**
   - **Game development with Unity**
3. Install **Windows 10 SDK (10.0.18362.0 or later)**

### Step 4: Install Mixed Reality Toolkit

1. Download **MRTK 2.7** or later from:
   ```
   https://github.com/microsoft/MixedRealityToolkit-Unity/releases
   ```
2. Import the following packages into Unity:
   - `Microsoft.MixedReality.Toolkit.Unity.Foundation.unitypackage`
   - `Microsoft.MixedReality.Toolkit.Unity.Extensions.unitypackage`
   - `Microsoft.MixedReality.Toolkit.Unity.Examples.unitypackage` (optional)

### Step 5: Open the Project in Unity

1. Launch **Unity Hub**
2. Click **"Add"** and navigate to the cloned repository folder
3. Select the project and open with the appropriate Unity version
4. Wait for Unity to import all assets (this may take several minutes)

### Step 6: Configure Build Settings

1. In Unity, go to **File → Build Settings**
2. Select **Universal Windows Platform**
3. Click **"Switch Platform"**
4. Configure UWP settings:
   - **Target Device**: HoloLens
   - **Architecture**: ARM64
   - **Build Type**: D3D Project
   - **Target SDK Version**: Latest installed
   - **Minimum Platform Version**: 10.0.18362.0 or higher
   - **Build Configuration**: Release (for deployment) or Master (for publishing)

### Step 7: Configure MRTK

1. In Unity menu, select **Mixed Reality Toolkit → Configure**
2. Apply the MRTK configuration for HoloLens 2
3. Update scenes to use MRTK:
   - Add **MixedRealityToolkit** component to scene
   - Add **MixedRealityPlayspace** for camera setup

---

## 📊 Dataset

### Data Location

Place the VAST Challenge 2019 dataset files in the following directory:
```
Assets/Resources/Data/VASTChallenge2019/
```

### Required Data Files

#### Mini-Challenge 1 (Earthquake Damage Reports)
```
mc1-reports-data.csv
```

**Format**:
```csv
time,location,shake_intensity,sewer_and_water,power,roads_and_bridges,medical,buildings
2020-04-06 00:00:00,1,5,3,4,2,6,5
```

**Fields**:
- `time`: Timestamp (YYYY-MM-DD HH:MM:SS)
- `location`: Neighborhood ID (1-19)
- `shake_intensity`: Perceived earthquake intensity (1-10)
- `sewer_and_water`: Infrastructure damage level (1-10)
- `power`: Power system status (1-10)
- `roads_and_bridges`: Road/bridge condition (1-10)
- `medical`: Medical facility status (1-10)
- `buildings`: Building damage level (1-10)

#### Mini-Challenge 2 (Radiation Sensor Data)

**Static Sensors**:
```
mc2-static-sensor-data.csv
```

**Mobile Sensors**:
```
mc2-mobile-sensor-data.csv
```

**Format**:
```csv
timestamp,sensor_id,longitude,latitude,value,units,user_id
2020-04-06 00:00:00,Sensor1,-115.123,35.456,0.05,cpm,
```

**Fields**:
- `timestamp`: Measurement time
- `sensor_id`: Unique sensor identifier
- `longitude`: GPS longitude coordinate
- `latitude`: GPS latitude coordinate
- `value`: Radiation measurement
- `units`: Measurement units (cpm - counts per minute)
- `user_id`: Mobile sensor operator ID (mobile sensors only)

### Downloading the Dataset

If you don't have the dataset:

1. Visit the official VAST Challenge 2019 page:
   ```
   https://vast-challenge.github.io/2019/
   ```
2. Download the datasets for Mini-Challenge 1 and Mini-Challenge 2
3. Extract and place in `Assets/Resources/Data/VASTChallenge2019/`

---

## 🎮 Usage Guide

### Running in Unity Editor (Play Mode)

1. **Open Main Scene**:
   ```
   Assets/Scenes/MainVisualization.unity
   ```

2. **Enable MRTK Simulation**:
   - In Game view, enable **"Unity Remote"** for testing
   - Or use **MRTK Input Simulation Service** for keyboard/mouse control

3. **Keyboard Controls in Play Mode**:
   - **W/A/S/D**: Move camera
   - **Q/E**: Move up/down
   - **Right Mouse + Drag**: Rotate view
   - **Left Mouse**: Simulate hand interaction
   - **Space**: Simulate air tap
   - **Tab**: Toggle input simulation panel

4. **Press Play** to start the visualization

### Deploying to HoloLens 2

#### Method 1: USB Deployment (Recommended for Development)

1. **Build the Unity Project**:
   - Go to **File → Build Settings**
   - Click **"Build"**
   - Select an output folder (e.g., `Builds/HoloLens2`)
   - Wait for build to complete

2. **Open in Visual Studio**:
   - Navigate to build folder
   - Open the `.sln` file in Visual Studio 2019

3. **Deploy to Device**:
   - Connect HoloLens 2 via USB
   - In Visual Studio toolbar:
     - Set configuration to **Release** or **Master**
     - Set platform to **ARM64**
     - Set target to **Device**
   - Click **Debug → Start Without Debugging** (Ctrl+F5)

4. **Wait for Deployment**:
   - First deployment may take 10-15 minutes
   - Subsequent deployments are faster

#### Method 2: Wi-Fi Deployment (Recommended for Testing)

1. **Enable Developer Mode on HoloLens**:
   - Go to **Settings → Update & Security → For developers**
   - Enable **Developer Mode**
   - Enable **Device Portal**

2. **Get HoloLens IP Address**:
   - Say **"What's my IP address?"** to Cortana
   - Or check in **Settings → Network & Internet → Wi-Fi → Hardware properties**

3. **Build and Deploy**:
   - Build Unity project as described above
   - In Visual Studio:
     - Set target to **Remote Machine**
     - Enter HoloLens IP address
     - Set authentication to **Universal (Unencrypted Protocol)**
   - Deploy

#### Method 3: App Package Installation

1. **Create App Package**:
   - In Visual Studio: **Project → Publish → Create App Packages**
   - Select **Sideloading**
   - Configure certificate and versions
   - Build package

2. **Install via Device Portal**:
   - Open browser to `https://[HoloLens-IP]`
   - Go to **Views → Apps**
   - Upload and install the `.appx` package

---

## 👋 Interaction Methods

### Hand Gesture Controls

#### View Manipulation
- **Grab and Move**: 
  - Open hand near visualization → close fingers to grab → move hand to reposition
  
- **Resize**:
  - Grab with both hands → move hands apart/together to scale
  
- **Rotate**:
  - Grab and twist hand to rotate around axis

#### Time Navigation
- **Slicing Plane**:
  - Grab the temporal slicing plane
  - Move hand forward/backward along time axis
  - Visual feedback shows current time period

#### Data Selection
- **Air Tap**: Point and pinch fingers to select data points
- **Far Interaction**: Ray from hand for distant object selection

### Voice Commands

#### View Control
```
"Show bar chart"
"Hide histogram"
"Display scatterplot"
"Show all views"
"Clear visualizations"
```

#### Data Categories
```
"Show shake intensity"
"Display power data"
"Show medical facilities"
"Display roads and bridges"
```

#### Time Control
```
"Play timeline"
"Pause"
"Reset time"
"Speed up"
"Slow down"
```

#### Spatial Audio
```
"Enable sound"
"Disable sound"
"Increase volume"
"Mute audio"
```

### Spatial Audio Feedback

- **Data Attributes**: Each category has unique auditory icons
- **Directional Audio**: Sound positioned in 3D space relative to data location
- **Temporal Changes**: Audio cues for time progression
- **Aggregation Alerts**: Distinct sounds for grouped data

---

## 📁 Project Structure

```
CodeControlVASTChallenge2019/
│
├── Assets/
│   ├── Scenes/
│   │   ├── MainVisualization.unity       # Primary MR scene
│   │   ├── CalibrationScene.unity        # Device calibration
│   │   └── WebGLComparison.unity         # 2D comparison scene
│   │
│   ├── Scripts/
│   │   ├── DataProcessing/
│   │   │   ├── CSVParser.cs              # Data loading
│   │   │   ├── DataCleaner.cs            # Data preprocessing
│   │   │   └── TemporalIndexer.cs        # Time series indexing
│   │   │
│   │   ├── Visualization/
│   │   │   ├── MapPlot.cs                # Spatial time series view
│   │   │   ├── BarChartController.cs     # Bar chart rendering
│   │   │   ├── ScatterPlotController.cs  # 3D scatterplot
│   │   │   └── SlicingPlane.cs           # Temporal navigation
│   │   │
│   │   ├── Interaction/
│   │   │   ├── GestureHandler.cs         # Hand gesture processing
│   │   │   ├── VoiceCommandManager.cs    # Speech recognition
│   │   │   └── ObjectManipulator.cs      # View manipulation
│   │   │
│   │   ├── Audio/
│   │   │   ├── SpatialAudioManager.cs    # 3D audio positioning
│   │   │   ├── DataSonification.cs       # Data-to-sound mapping
│   │   │   └── AuditoryIcons.cs          # Sound icon library
│   │   │
│   │   └── Utilities/
│   │       ├── CoordinateMapper.cs       # Geo to 3D space mapping
│   │       ├── ColorScale.cs             # Color encoding
│   │       └── TimeController.cs         # Time progression logic
│   │
│   ├── Materials/
│   │   ├── DataPointMaterial.mat         # Data visualization shaders
│   │   ├── HolographicMaterial.mat       # MR-optimized materials
│   │   └── GlowEffect.mat                # Highlight effects
│   │
│   ├── Prefabs/
│   │   ├── VisualizationViews/
│   │   │   ├── BarChartView.prefab
│   │   │   ├── ScatterPlotView.prefab
│   │   │   └── MapPlotView.prefab
│   │   │
│   │   ├── UI/
│   │   │   ├── ControlPanel.prefab       # UI control panel
│   │   │   └── TimelineSlider.prefab     # Time navigation UI
│   │   │
│   │   └── DataMarkers/
│   │       ├── DataGlyph.prefab          # 3D data point marker
│   │       └── SensorIcon.prefab         # Sensor location marker
│   │
│   ├── Resources/
│   │   ├── Data/
│   │   │   └── VASTChallenge2019/        # Dataset CSV files
│   │   │
│   │   └── Audio/
│   │       └── AuditoryIcons/            # Sound files
│   │
│   ├── Shaders/
│   │   ├── HologramShader.shader         # Holographic rendering
│   │   └── DataVisualization.shader      # Custom visualization shaders
│   │
│   └── MRTK/                              # Mixed Reality Toolkit files
│
├── Packages/
│   ├── manifest.json                      # Package dependencies
│   └── packages-lock.json
│
├── ProjectSettings/
│   ├── ProjectSettings.asset
│   └── ...                                # Unity project configuration
│
├── .gitignore
└── README.md
```

---

## 🏗️ Architecture

### Data Processing Pipeline

```
1. CSV Data Loading
   ↓
2. Data Cleaning & Validation
   ↓
3. Temporal Indexing
   ↓
4. Spatial Mapping (Geo → Unity Coordinates)
   ↓
5. Data Aggregation & Statistics
   ↓
6. Visual Encoding (Color, Size, Position)
   ↓
7. Audio Encoding (Pitch, Volume, Spatial)
   ↓
8. Rendering (3D Meshes, Particles, Shaders)
```

### Interaction Flow

```
User Input (Hand/Voice)
   ↓
MRTK Input System
   ↓
Gesture/Voice Handler
   ↓
Visualization Controller
   ↓
Data Query & Filter
   ↓
Update Visual/Audio Output
   ↓
Spatial Rendering (HoloLens 2)
```

### Multiple Coordinated Views System

- **View Manager**: Coordinates all visualization views
- **Event System**: Links interactions across views
- **Brushing and Linking**: Selections propagate across views
- **Synchronized Time**: All views share temporal state

---

## 🔬 Research Background

### Immersive Analytics

This project contributes to the emerging field of **immersive analytics**, which explores data analysis in immersive environments. Key research themes include:

- **Embodied Interaction**: Using the human body as an input device
- **Spatial Cognition**: Leveraging 3D space for data understanding
- **Multimodal Perception**: Combining visual, and auditory feedback
- **Natural User Interfaces**: Gesture and voice for intuitive interaction

### Design Principles

#### Human-Centered Approach
- Prioritizes natural human abilities (spatial reasoning, parallel listening)
- Reduces cognitive load through spatial organization
- Enables physical movement around data

#### Multiple Coordinated Views
- Traditional InfoVis technique adapted for 3D space
- Views positioned in surrounding physical space
- User-customizable spatial layout

#### Temporal Navigation
- Video-like playback of time-series data
- User-controlled progression through slicing plane
- Maintains context across time periods

#### Spatial Sound
- Eyes see forward, ears hear 360°
- Superior temporal change detection via audio
- Parallel listening in busy visualizations

---

## 📚 Related Publications

This work builds upon research presented in:

**Sardana, D., Kahu, S. Y., Gračanin, D. & Matković K. (2021)**  
*"Multi-modal Data Exploration in a Mixed Reality Environment Using Coordinated Multiple Views"*  
In: Human Interface and the Management of Information. HCII 2021.  
Lecture Notes in Computer Science, vol 12766. Springer, Cham.  
[https://doi.org/10.1007/978-3-030-78321-1_26](https://doi.org/10.1007/978-3-030-78321-1_26)

### Dissertation

**Sardana, D. (2023)**  
*"Embodied Data Exploration in Immersive Environments: Application in Geophysical Data Analysis"*  
Doctoral Dissertation, Virginia Polytechnic Institute and State University.  
[Available online](https://vtechworks.lib.vt.edu/items/6f22f081-e3ae-4ce6-a128-70c4dd23e5bb)


**Key Contributions:**
- Novel map-plot view for spatial time series analysis
- Comparative study: immersive MR vs. conventional 2D displays
- Multi-modal interaction techniques (gesture, voice, spatial audio)
- Application to real-world disaster response dataset

---

## 🐛 Troubleshooting

### Common Issues and Solutions

#### Build Errors

**Problem**: "The type or namespace name 'UnityEditor' could not be found"
```
Solution: 
1. Check that files using UnityEditor are in Editor folders
2. Wrap editor code in #if UNITY_EDITOR directives
```

**Problem**: "Failed to build app bundle"
```
Solution:
1. Ensure .NET backend is not selected (use IL2CPP)
2. Update to latest Windows 10 SDK
3. Clean and rebuild in Visual Studio
```

#### Deployment Issues

**Problem**: Cannot connect to HoloLens via USB
```
Solution:
1. Enable Developer Mode on HoloLens
2. Update USB drivers on PC
3. Try different USB port (USB 3.0 required)
4. Pair device in Windows Device Portal
```

**Problem**: App crashes on startup on device
```
Solution:
1. Check ARM64 build configuration
2. Verify InternetClient capability is enabled
3. Check for missing dependencies in Package.appxmanifest
4. Review Visual Studio output for DLL loading errors
```

#### MRTK Issues

**Problem**: Hand tracking not working
```
Solution:
1. Verify hand tracking is enabled in MRTK profile
2. Check that scene has MixedRealityToolkit component
3. Ensure proper input data provider configuration
4. Calibrate hand tracking on HoloLens (Settings → System → Calibration)
```

**Problem**: Voice commands not recognized
```
Solution:
1. Enable microphone capability in Unity project settings
2. Train Cortana voice recognition on HoloLens
3. Speak clearly and at normal pace
4. Check SpeechCommandsProfile in MRTK configuration
```

#### Performance Issues

**Problem**: Low frame rate on HoloLens
```
Solution:
1. Reduce polygon count in 3D models
2. Use LOD (Level of Detail) for distant objects
3. Optimize shader complexity
4. Implement frustum culling
5. Batch draw calls
6. Reduce particle count in visualizations
```

**Problem**: Data loading is slow
```
Solution:
1. Preprocess and cache data in binary format
2. Implement lazy loading for large datasets
3. Use coroutines to avoid blocking main thread
4. Consider data decimation/aggregation
```

#### Data Issues

**Problem**: CSV parsing errors
```
Solution:
1. Verify CSV file encoding (UTF-8 recommended)
2. Check for missing values or malformed rows
3. Ensure date/time format matches parser expectations
4. Validate data types in each column
```

---

## 🤝 Contributing

We welcome contributions from the research community! Here's how you can help:

### Reporting Issues

1. Check existing issues first
2. Provide detailed description of the problem
3. Include steps to reproduce
4. Share error messages and logs
5. Specify Unity/HoloLens versions

### Suggesting Enhancements

1. Open an issue with label "enhancement"
2. Describe the proposed feature clearly
3. Explain the use case and benefits
4. Provide mockups or examples if possible

### Pull Requests

1. Fork the repository
2. Create a feature branch:
   ```bash
   git checkout -b feature/amazing-new-feature
   ```
3. Make your changes following coding standards:
   - Use meaningful variable names
   - Comment complex logic
   - Follow C# coding conventions
   - Add XML documentation for public methods
4. Test thoroughly on HoloLens 2
5. Commit with clear messages:
   ```bash
   git commit -m "Add: New temporal filtering algorithm"
   ```
6. Push to your fork:
   ```bash
   git push origin feature/amazing-new-feature
   ```
7. Open a Pull Request with:
   - Clear description of changes
   - Screenshots/videos if UI-related
   - Testing notes

### Development Guidelines

- **Code Style**: Follow Microsoft C# Coding Conventions
- **Comments**: Use XML documentation for public APIs
- **Testing**: Test on actual HoloLens 2 hardware when possible
- **Performance**: Profile and optimize critical paths
- **Accessibility**: Consider users with different abilities

---

## 📄 License

This project is available for educational and research purposes. 

**Dataset License**: The VAST Challenge 2019 dataset is provided by the IEEE VAST Challenge organizers. Please refer to the [official VAST Challenge website](https://vast-challenge.github.io/2019/) for data usage terms.

**Code License**: MIT License
\
Copyright (c) 2026 Disha Sardana
\
Permission is hereby granted, free of charge, to any person obtaining a copy... (see the full text in [LICENSE](https://github.com/disha13sardana/CodeControlVASTChallenge2019/blob/main/LICENSE))

**When using this work, please cite:**

```bibtex

@phdthesis{sardana2023embodied,
  title={Embodied Data Exploration in Immersive Environments: Application in Geophysical Data Analysis},
  author={Sardana, Disha},
  year={2023},
  school={Virginia Polytechnic Institute and State University}
}

@inproceedings{sardana2021multimodal,
  title={Multi-modal Data Exploration in a Mixed Reality Environment Using Coordinated Multiple Views},
  author={Sardana, Disha and Kahu, Sampanna Yashwant and Gra{\v{c}}anin, Denis},
  booktitle={Human Interface and the Management of Information. Information Presentation and Visualization: Thematic Area, HIMI 2021},
  pages={331--352},
  year={2021},
  organization={Springer}
}
```

---

## 👤 Authors

**Disha Sardana**  
- GitHub: [@disha13sardana](https://github.com/disha13sardana)
- ORCID: [0000-0002-0137-4982](https://orcid.org/0000-0002-0137-4982)

**Contributors**:
- Sampanna Yashwant Kahu - ORCID: [0000-0002-8522-2926](https://orcid.org/0000-0002-8522-2926)
- Denis Gračanin - ORCID: [0000-0001-6831-2818](https://orcid.org/0000-0001-6831-2818)

---

## 🔗 Useful Links

### Project Resources
- **Demo**: [YouTube/multimodal-data-exploration](https://www.youtube.com/watch?v=y1DOrvwNDso&t=2s)
- **Project Website**: [disha-sardana/work/immersive-analytics](https://disha-sardana.squarespace.com/work/immersive-analytics)  
- **IEEE VAST Challenge 2019**: [https://vast-challenge.github.io/2019/](https://vast-challenge.github.io/2019/)

### Development Resources
- **Microsoft HoloLens 2 Documentation**: [https://docs.microsoft.com/hololens/](https://docs.microsoft.com/hololens/)
- **Mixed Reality Toolkit (MRTK)**: [https://github.com/microsoft/MixedRealityToolkit-Unity](https://github.com/microsoft/MixedRealityToolkit-Unity)
- **Unity for HoloLens**: [https://learn.microsoft.com/windows/mixed-reality/develop/unity/unity-development-overview](https://learn.microsoft.com/windows/mixed-reality/develop/unity/unity-development-overview)
- **MRTK Documentation**: [https://docs.microsoft.com/windows/mixed-reality/mrtk-unity/](https://docs.microsoft.com/windows/mixed-reality/mrtk-unity/)

### Tutorials
- **HoloLens 2 Tutorials**: [Microsoft Learn](https://learn.microsoft.com/training/paths/beginner-hololens-2-tutorials/)
- **MRTK Hand Tracking**: [MRTK Hand Interaction Examples](https://docs.microsoft.com/windows/mixed-reality/mrtk-unity/features/input/hand-tracking)
- **Unity Optimization for HoloLens**: [Performance Recommendations](https://docs.microsoft.com/windows/mixed-reality/develop/unity/performance-recommendations-for-unity)

---

## 📧 Contact

For questions, feedback, or collaboration opportunities:

- **Issues**: Please use [GitHub Issues](https://github.com/disha13sardana/CodeControlVASTChallenge2019/issues)
- **Inquiries**: Contact through email: disha13sardana@gmail.com
- **General Questions**: Open a discussion in the repository

---

## 🙏 Acknowledgments

- **IEEE VAST Challenge organizers** for providing the comprehensive dataset
- **Microsoft** for HoloLens 2 hardware and MRTK framework
- **Visual Analytics research community** for foundational work in immersive analytics
- **Virginia Tech ICAT** for research support and facilities - [https://icat.vt.edu/](https://icat.vt.edu/)
- All contributors and participants who helped improve this system

---

## 🎓 Educational Use

This project is ideal for:

- **Graduate Research**: Immersive analytics, visual analytics, HCI
- **Course Projects**: Mixed reality development, data visualization
- **Workshops**: Hands-on MR development training
- **Demonstrations**: Showcasing immersive data analysis techniques

Feel free to use this project in research and educational settings. We appreciate attribution and would love to hear about your experiences!

---

**Note**: This is a research prototype developed for exploring novel interaction techniques in immersive visual analytics. While functional, it may require adaptation for production use cases.

**Last Updated**: February 2026  
**Version**: 1.0.0
