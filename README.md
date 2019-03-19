# The Potato Benchmark Project

Potato Benchmark is an benchmark project to make it easy to compare performance between LWRP and Built-in pipelines. It contains some baseline performance test scenes that specifically stress parts of the graphics pipeline such as test fill rate of built-in shaders, batching/driver cost and bandwidth. 

The benchmark is still WIP. It builds for Android (GLES2, GLES3, Vulkan) and iOS (Metal) a set of test scenes in LWRP and Built-in. A main scene allows to select which pipeline to test. Each tests scene warms up for 2s. and measure frame times (average, median, min, max) in a period of 8s.

# How to use it

## Building to mobile

In the Unity toolbar select `Tools` then select the target API to built.
This will either generate an .APK file or an XCode project.

## Building to other platforms

There's no custom build script for other platforms other than mobile. You have to switch manually to the platform and build scenes. That's it. 

## With Test Runner

Go to `Window -> General -> Test Runner`. Then tests can run on either editor or deploy to target device.
Then to see results go to `Window -> Analysis -> Performance Test Report`

# Running Tests

An main menu will display two buttons to select with Pipeline benchmark tests to run. Results will be outputted to console. 
You can filter console output with `[Benchmark]`. F.ex on in Android use the following command line: `adb logcat -s Unity | grep [Benchmark]`.

Note: If you are running in mobile, please remember to give 2min rest between each pipeline test.

# Detail about current Tests


## Fill rate tests
Stress GPU fragment (ALU/Texture) by rendering a native device resolution set of fullscreen quads. Test is designed to achieve 2.5x overdraw which is acceptable rate for a mobile game.
The purpose of the fill rate tests is to compare performance of single vs multiple passes strategies for each pipeline built-in shaders in different devices.
 - Lit shader (Directional Light Only)
 - Lit shader (1 Directional + 4 point lights per-pixel)

## Draw Call (GPU)

Stress GPU by issueing a considerable number of drawcalls that can be batched. 

Tests have about 1k block building and one directional shadow casting light + PCF Filtering.
The purpose of the realtime shadow test scene is to compare how each pipeline handles shadow culling, rendering and depth pre-pass.

Test with and without cascades.
 - Realtime Shadows
 - Realtime Shadows Cascades

## Draw call (CPU) 
Test batching/cost of drawcall setup by stressing CPU with 2.7k simple drawcalls each with a different materials.
The purpose of this tests scene is to compare the SetPass cost for each pipeline. 

## Bandwidth
Stress bandwidth cost.

The purpose of this test is to figure out points of improvements in terms of bandwidth cost to have a very lean pipeline in term of LOAD/STORE. Currently we have a lot of room for improvement in terms of bandwidth when using MSAA.

# Credits

- Spawn Object script adapted from Arnaud's Batcher.
