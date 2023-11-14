Journal
============

2023-10-18
-------------
#### 4219 Frågesport Net Core - A och B.
I have finished part A and B, so you can now manually enter regular and 
Multiple Choice Single Answer (MCSA) quiz questions. These are saved in a list,
and when you play the game, a random card is picked, read, and discarded. 
Your score is displayed while you play, and you get a message based on your score at the end.
The message is simple: either you get a perfect score, all wrong or something in between.
I didn't struggle particularly much with anything, but I had to stop and think for a bit
when making the class for the MCSA questions. Using inheritance, I created something I think works fine.
I used the following links throughout part A and B:
* https://stackoverflow.com/questions/21345005/add-multiple-items-to-a-list
* https://stackoverflow.com/questions/19318430/c-sharp-select-random-element-from-list
* https://learn.microsoft.com/en-us/dotnet/api/system.string.split?view=net-7.0
* https://stackoverflow.com/questions/983030/type-checking-typeof-gettype-or-is

Using a few various techniques, I managed to make the program fairly robust. You can't enter
a number when the answer requires text, and you can't enter text when the answer requires a number.
Questions that require multiple words also work fairly well, where the program splits the required answer 
into separate strings and checks to make sure that every word in the answer appears at least once in the 
submitted answer. For example, the question "What is Eyjafjallajökull, and where is it located?", where 
the answer required is "volcano iceland", will require the user to type in anything that has the words
"volcano" and "iceland" somewhere in it. This method is fairly simple, but it comes with some drawbacks.
However, for simple reasons such as this, it works well enough. After this, I intend to add more questions,
make it possible for the player to add their own questions, and make sure that even if you misspell something
slighly, the answer will still pass.

2023-11-14
--------------
#### 4131 WPF Filhantering
I continued working on the quiz program, both with a UI and with file reading. The program now reads the questions from a file,
and it works perfectly, I think. Each question is read, and then a new question is created and added to a list depending 
on the type of question. I am currently trying to add a method that resets the deck, as currently you can only play the quiz
once per run of the program. Since the deck removes each card from itself after the card is played, the deck runs out of
cards after each play. I'm trying to update the deck variable in UserInterface with `deck = new Deck();`, but it doesn't seem
to updpate deck in Quiz despite UserInterface calling Quiz's constructor with the deck as an argument. This process involves
a few steps, so I'm not certain exactly what is going wrong where. The obvious problem is that deck in Quiz doesn't update 
accordingly, though.