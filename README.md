TextStyleManager
================

TextStyleManager is a Unity asset to help with managing the fonts that are applied to TMPro_Text components in a project, and supporting quickly changing all the fonts that are being used at runtime. The initial use case for this project was to make it easy to switch between sets of fonts for use with different languages when implementing localization. It also facilitates changing the fonts being used throughout a project trivial, while making it very explicit what the set of distinct text presentation types are in use.

Screenshots
-----------

Simple stylesheet creation process:

![GIF of editing stylesheet](https://i.imgur.com/m9zfGWC.gif)

Preview Stylesheets live:

![GIF of stylesheet changing in Edit mode](https://i.imgur.com/fqkv5lq.gif)

Dynamically restyle labels:

![GIF of styleshet changing in Play mode](https://i.imgur.com/rjdpLfF.gif)

Layout
------

* `TextStyle` - Defines the properties of a text style that can be applied to a TMPro label. This is currently the font, material, and whether the text should be left-to-right or right-to-left.
* `TextStyleType` - Defines a class of labels that should share a style, eg. "MenuLabel", "HUDLabel". These do not get created directly, the `TextStyleSet` editor manages these automatically.
* `TextStyleSet` - Declares the set of TextStyleTypes that are active in a project. At runtime it also tracks what map is currently in use. This object stores `TextStyleType`s as sub-assets.
* `TextStyleMap` - Defines a mapping from the types declared in a TextStyleSet to TextStyles. We might make a map for the fonts to use with English/German/Spanish, another for Japanese, and another for Arabic.
* `TextStyleSwitcher` - Component to place on TMPro labels. Apply this component, select a StyleSet, and then select a Text Type

Basic Usage
-----------

1. Create a `TextStyleSet` using the `Create/TextStyleManager/Text Style Set` object creation menu.
2. On the created `TextStyleSet` declare the `TextStyleType`s for the project, using the "Style Types" list.
3. Create a `TextStyleMap` using the `Create/TextStyleManager/Text Style Map` object crteation menu.
4. On the created `TextStyleMap` select the previously created `TextStyleSet`.
5. Create `TextStyle`s for each of the styles you need using the `Create/TextStyleManager/Text Style` object creation menu.
6. Connect these `TextStyles` to `TextStyleMap`.
7. Set the `TextStyleMap` as the startup style map on the `TextStyleSet`.
8. Add `TextStyleSwitcher`s to TMPro components in the project, and select the `TextStyleSet` and style type.

Scripting
---------

The main method of interest is `TextStyleSet::SetActiveStyleMap`. This will update the stylemap in use at runtime, and all `TextStyleSwitcher`s will update the settings of the TMPro Text component they are managing.

-----------------------------------

<!--<a href="http://patreon.com/pulponitemakes/"><img src="https://c5.patreon.com/external/logo/become_a_patron_button.png" width="15%" height="15%"></a>-->
