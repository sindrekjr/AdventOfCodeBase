define newline


endef

YEAR ?= $(shell date +%Y)
DAYS ?= $(shell seq 1 25)

TEMPLATE := $(subst <YEAR>,$(YEAR),$(subst $(newline),\n,$(file < ./templates/solution.cs.template)))
DIRECTORY := "$(shell pwd)/AdventOfCode.Solutions/Year$(YEAR)"

solutions-files: $(DAYS)

$(DAYS):
	$(eval DAY=$(shell printf "%02d" $@))
	$(eval DAY_DIR="$(DIRECTORY)/Day$(DAY)")
	$(eval DAY_FILE="$(DAY_DIR)/Solution.cs")
	@if [ ! -f $(DAY_FILE) ]; then \
		mkdir -p $(DAY_DIR); \
		echo '$(subst <DAY>,$(DAY),$(TEMPLATE))' > $(DAY_FILE); \
	fi
