get-childitem -r -include /bin/,/obj/ | foreach($_){ remove-item $_ -r }