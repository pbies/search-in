	Search for patterns in file

	Use:

	search-in file1 file2 > file3

	Where:

	file1 - file with patterns to search for (mostly public addresses, one per line); this file
		is loaded into memory

	file2 - file with public addresses # private keys # brainwallets - in this file the patterns
		will be searched using StartsWith function (beginning of line, makes it fast); this file
		is read line by line

	file3 - file with result - found file1 lines in file2, file2 line
		is printed

	Program prints progress (number of lines processed) to standar error output.
