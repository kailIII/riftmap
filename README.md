RiftMap
by: Sam Halperin 
sam@samhalperin.com
http://www.github/shalperin

Notes
=====
See the issues on github, there are some fairly serious open problems with 
this software.  (It's a proof of concept.)  Particularly in the tesselation
algorithm.  Map kind of looks like swiss cheese right now.

How To See The Demo
===================
Look in in the builds folder at the root of the project for an executable.


How To Modify The Program For Your Use
======================================
+ Open the project in unity.
+ There are GeoJSON files in Assets/GeoJSON
+ There are example data files in Assets/Data
+ Select the game object Scripts/StateShapeTestRoot in the hierarchy. Look at the inspector:
    * Drag a GeoJSON file onto the GEOJson field.
    * Drag a data file onto the Data field.
+ Back in the hierarchy, in CharacterControllers, there are 2 - one is for the oculus rift, and one is
not.  Enable one or the other as needed.
+ Build for your platform.
