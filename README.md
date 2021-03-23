# Image Rename

**Problem Statement**

After upgrading to node 14, hexo blogging framework no longer parses images with spaces in the name.

**Possible solutions**

* Wrap markdown with quotes
* Rename the images and replace a space with a dash

The rename felt like the best future proof approach, the downside was the markdown files also needed to be updates.

**Acceptance Criteria**

1. identify all images in `C:\Dev\blog2\carlpaton.github.io\public\d` that have a space in the name
2. record the folder name as this matches the markdownfile, dump results to json as 

```
{
	"folderName": null,
	"images": [{
		"beforeName": null,
		"afterName": null
	}]
}
```

3. update the files to replace 

```
SPACE with dash
UNDERSCORE with dash
```

4. update file names to all be lower case (small annoyance I can fix now)
5. must be idempotent