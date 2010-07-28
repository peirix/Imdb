 Feature: Movie List on Home page
	In order to find good movies
	As a movie lover to keep up to date on the top 250 movies
	I want to see top movies according to IMDb users
	
	Scenario: List top movies on home page
		Given I am on the home page
		Then I should see the following movies
			| Title |
			| The Shawshank Redemption |
			| The Godfather |
			| Inception |
			| The Godfather, Part II |
			| The Good, the Bad and the Ugly |
	