SPACE :=
SPACE +=
COVERAGE_FILES=$(shell find . -name coverage.xml)
REPORTS=$(subst $(SPACE),;,$(COVERAGE_FILES))

build:
	dotnet build
test: build
	dotnet test
coverage:
	dotnet test /p:AltCover=true
coverage-report: coverage
	reportgenerator "-reports:$(REPORTS)" "-targetdir:coveragereport"
publish:
	dotnet publish -c Release --self-contained -r alpine.3.7-x64 -o ./output GravityMain.Web/
restore:
	dotnet restore
run:
	dotnet run --project GravityMain.Web/
clean:
	dotnet clean
	find . -name '*.orig' -exec rm {} \;
	find . -name 'coverage.xml' -exec rm {} \;
	rm -rf $$(find . -name 'bin')
	rm -rf $$(find . -name 'obj')
	rm -rf coveragereport
list-packages:
	grep PackageReference --include=*.csproj -r * | cut -d ' ' -f 6-7 | cut -d '"' -f 2,4 | tr '"' ' ' | sort | uniq
format:
	dotnet format
format-test:
	dotnet format --dry-run --check -v n
