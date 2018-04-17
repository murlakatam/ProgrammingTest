# 2 Card Poker Challenge

This programming challenge is designed to see how you code, it should be able to 
an hour and include input validation and unit tests to prove your code will work 
as expected.

The code can either be delivered via a public repo or a zip file.

Please include a readme file with how to compile and run the project and any
related notes.

## Requirements

### Develop a simplified 2 card poker game to show off your C# programming prowess.

  1. 2-6 players.
  2. 2-5 rounds.
  3. The dealer shuffles the deck at the start of each round.
  4. The dealer deals 2 cards to each player.
  5. The dealer ranks each player’s hand according the poker hand ranking rules
  6. At the end of each round, each player is assigned a score (0 – weakest to 
     strongest x-1 (where x = number of players)).
  7. The overall winner is determined once all rounds have been completed. The 
     winner is the player with the highest score.

### Poker Hand Ranks:

In order from strongest to weakest

  1. Straight Flush (2 cards of sequential rank, same suit)
  2. Flush (2 cards, same suit)
  3. Straight (2 cards of sequential rank, different suit)
  4. 1 pair (2 cards of same rank)
  5. High Card (2 cards, different rank, suit and not in sequence. Highest card wins)
    * Individual cards are ranked A (highest), K, Q, J, 10, 9, 8, 7, 6, 5, 4, 3, 2 (lowest).
    * Suit order (strongest to weakest): Spades, Clubs, Hearts, Diamonds

### Objective:

Develop a 2 card poker game according to the rules above. Implement each feature 
according to the acceptance criteria stated later. You will be judged on the 
following merits:
  
  1. Code quality
  2. Test coverage
  3. Correctness (according to the game specification)

#### Feature: Shuffle Deck

**As** The Dealer 

**I want to** Shuffle the Deck

**So that** the card sequence is different for each round

**Scenario:** Shuffle Deck X Times

Given it is the start if a new round

And the game is not over

And a deck of 52 cards

When I shuffle the deck X time (s)

Then the deck is in a different order each time
