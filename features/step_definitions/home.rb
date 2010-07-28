require 'watir'
require 'spec/expectations'

browser = Watir::IE.new

Before do
	@browser = browser
end

at_exit do
	browser.close
end

Given /^I am on the home page$/ do
	@browser.goto "http://localhost:51131/"
end

Then /^I should see the following movies$/ do |table|
	table.hashes.each do |hash|
		@browser.text.should =~ hash["Title"].to_s
	end
end