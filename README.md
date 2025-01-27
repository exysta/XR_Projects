# XR_Projects
Repository containing projects done for the XR class at the University of Oulu for the spring semester of 2025.

---

## Project 1: TheRoom
**Description**:  
A simple project to start working on VR development. This project features:
- A room with different wall textures and a skybox.
- Player interactions including:
  - Moving around the scene.
  - Changing the room's light color.
  - Teleporting inside/outside the room using controller buttons.

---

## Project 2: ScopeAndGrab
For practical reasons, this project is divided into two sections: **Scope** and **Grab**.

### Scope
**Description**:  
The scope section showcases a magnifying glass in VR with:
- A **zooming side** and an **unzooming side**.
- A hidden object in the scene (under the blue arrow) that can only be seen through the lens.

**Technical Details**:
- The lens effect is achieved using **render textures** and a **secondary camera**.
- The camera attached to the lens adjusts its orientation based on the player's camera, creating the effect of a "real lens."

---

### Grab
**Description**:  
This part implements a grabbing system **without using the XR Interaction Toolkit**.

**System Overview**:
- **Single-Handed Grabbing**:
  - The object translates by the same vector and rotates by the same quaternion as the controller.
  - Rotation of the controller results in the object translating around the controller's origin (similar to how the moon orbits Earth).
  - The vector from the controller to the object is rotated by the delta rotation quaternion.

- **Two-Handed Grabbing**:
  - Translation and rotation around both controllers are combined:
    - If controllers translate in opposite directions, the object remains stationary as translation vectors cancel out.
    - If controllers execute opposite rotations, no rotation is applied to the object.
  - Composite transformations:
    - **Translation**: Translation vectors from both controllers are added together.
    - **Rotation**: Quaternions from both controllers are multiplied to form a composite quaternion.

---

### Additional Feature: Double Rotation
**Description**:  
A feature to double the rotation applied to the grabbed object, enhancing rotation manipulation.

**Implementation**:
- After calculating the quaternion to be applied to the object, the magnitude of the rotation is doubled while keeping the rotation axis intact.
- This is achieved by:
  - Decomposing the quaternion into its angle-axis representation.
  - Doubling the angle while preserving the axis.
  - Reconstructing the quaternion with the modified angle.
- The feature is on for both hands by default but could easily be toggled by a user input in a later implementation.

**Goal**:  
To improve the grabbing system and make rotations more dynamic when desired.

---

