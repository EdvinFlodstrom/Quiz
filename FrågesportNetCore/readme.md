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

2023-11-17
--------------
#### 4131 WPF Filhantering
Deck now updates each time the quiz is initiated. I fixed this by moving the instance creation of the class Deck to Quiz from 
UserInterface.

I've continued with the program for a while now, and it's been a smooth process. Up until now, anyway. So, I solved the problem
I've been trying to fix for the last 15 minutes, but I am still going to note it down here because of how dumb my mistake was.
When trying to create a string with all the combiened options entered for an MCSA question, I had a string that could look 
like this: "1,2,3,4,5,". Note the "," at the end, it's what I was trying to get rid of. So I went to a Stack Overflow page, 
this one: https://stackoverflow.com/questions/3573284/trim-last-character-from-a-string. It recommended me to try 
`combinedMCSAOptionsString.Remove(combinedMCSAOptionsString.Length - 1);`, which I did. It didn't solve it though, so I 
apparently channeled my inner fool and removed "- 1". This, of course, throws an error because the Remove functions tries to
remove something that is outside the string in question. But, because I have a try-catch statement surrounding the code, 
including the code snippet I mentioned earlier, the catch statement catches the error. Since the catch statement in this case
is meant to stop you from entering the same option twice (I'm not sure if I should be doing it this way), the code doesn't
log an error for the message. Suffice to say, I didn't understand what the error was. After a while of using the debugger, I
came across the root of the issue (removing "- 1", this was not very smart), and I swiftly fixed the issue. To summarize: I
made a rather dumb mistake and it took me a ludicrous amount of time to fix it for what it was. So now I'll get back to what
I was trying to do at the start, removing the last ',' in the string I mentioned earlier. 

And it's fixed. I should probably check when methods actually modify the value they're called upon and not, because that's
what was confusing me here. I simply added `combinedMCSAOptionsString = ` before the 
`combinedMCSAOptionsString.Remove(combinedMCSAOptionsString.Length - 1);`. So now it looks like this: 
`combinedMCSAOptionsString = combinedMCSAOptionsString.Remove(combinedMCSAOptionsString.Length - 1);`.

2023-11-21
-------------
#### 4131 WPF Filhantering
Rather than continuing with adding more functions to the program, I've started polishing it a bit more. To start out,
I removed all try-catch statements as they were unnecessary and caused confusion. Rather than using try{} to convert
strings to integers, I'm now using `int.TryParse(answer, out intAnswer)`, which is Microsofts recommended way of 
doing it. It doesn't throw exceptions, so it makes the code more clear. I've also started separating the class
UserInterface into two, one containing only the interface itself, and the other, AcitonHandler, containing all the
functions of the UI. ActionHandler will be the class that saves data in variables and adds them together to create 
questions and some other things. Despite the fact that I've only yet created a method for creating questions, 
not one for deleting or modifying them, it still takes a lot of work to separate the current UI class. In the end,
though, the program should be much more clear and easily readable, which is preferable.

Some time later, I've now split UserInterface into two classes, more or less. Some of the functions that UserInterface
previously took care of, such as creating the string for the question to be added and calling a method in FileManager,
are now handled by ActionHandler instead. Although I didn't manage to completely and thoroughly separate the two, 
at least it's a little better now. As it is right now, UserInterface asks a few questions, saves the answers, 
and sends the answers as arguments to a method in ActionHandler that uses the parameters to concatenate them into
a single string, which is then sent as an argument to a method in FileManager. It took almost three hours, but
the code is now a lot better written.

And now the program can write to the 'quiz_questions.txt' file as a way to save user-made questions. These questions
appear as soon as you start a new quiz, be it right after adding a question or after restarting the program. It works
as intended, in other words. During this lesson, I used the following links:
* https://github.com/karlsson0214/WpfFileDemo/blob/master/WpfFileDemo/MainWindow.xaml.cs
* https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/type-testing-and-cast
* https://stackoverflow.com/questions/3160127/whats-the-point-of-as-keyword-in-c-sharp
* https://stackoverflow.com/questions/7042314/can-i-check-if-a-variable-can-be-cast-to-a-specified-type#7042384
* https://stackoverflow.com/questions/1722964/when-to-use-try-catch-blocks
* https://stackoverflow.com/questions/2344411/how-to-convert-string-to-integer-in-c-sharp