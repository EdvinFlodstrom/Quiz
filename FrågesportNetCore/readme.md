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

2023-11-22
-------------
#### WPF Filhantering
I've now also implemented question removing. The currently available options are now: 1. Take the quiz, 2. Add a
quesiton to the quiz, and 3. Remove a quesiton from the quiz. I was pondering several different approaches when it
came to removing questions from the quiz, and eventually used one that reads all the current lines from the quiz
and adds them to a string, unless the question being read has the same index as the index of the question the user
wants to remove (the user is asked to input the number of the question they want to remove). I then do this: 
`File.WriteAllText(pathAndFileName, newStringOfQuestions);`. Since there is already a document at pathAndFileName,
the file is overwritten with the contents of newStringOfQuestions, which is a single string containing all the
questions, answers and options of all the questions. In hindsight, I wonder if the string will ever get too large, 
but that's a problem for a later time, I think. It works, so that's a win in my books.
* https://stackoverflow.com/questions/33490890/write-the-data-to-specific-line-c-sharp
* https://stackoverflow.com/questions/668907/how-to-delete-a-line-from-a-text-file-in-c
* https://stackoverflow.com/questions/44235689/how-to-overwrite-a-file-if-it-already-exists
* https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealltext?view=net-8.0&redirectedfrom=MSDN#System_IO_File_WriteAllText_System_String_System_String_

A little bit later, now you can also modify questions in the quiz. For once, I daresay I handled it fairly well. 
Rather than adding a billion new methods for finding the correct question and extracting it and modifying it, I instead
rewrote a few methods, including the one that removes a question. So the method in UserInterface that modifies a 
question is only three lines long, and it's because it uses the methods for creating a question and removing
a question. So what happens when you modify a question is the following: 1. All questions are listed from 1-x. 
2. You choose which question to modify. 3. You add a new question (but the question is never added, rather, a string
with the question is returned for later use). 4. FileManager's `RemoveOrModifyQuestion(int numberOfQuestion, string modifiedQuestion = "")`
is called, with modifiedQuestion not being equal to "". 5. A streamreader reads the entire .txt file and saves
each line in a single string, EXCEPT for if the index of the line being read is the same as the one you chose
to modify. If that is the case, the program checks that `(modifiedQuestion != "")`. If this evaluates to true, 
the program adds the question you added earlier (I mentioned that it is saved for later use) to the string instead.
The program then continues the same way as when you delete a question: it overwrites the contents of the file with 
the string containing the new questions. In the end, the .txt file looks the exact same except the modified question
is replaced with a completely new question that you write yourself. When writing this, though, I thought of something.
If you try to enter the same answer for two different options in an MCSA (Multiple Choice Single Answer) question,
the program tells you "no" and returns to the menu. Problem is, the question you chose to modify is removed and 
not replaced with anything. I'll have to fix this real quick.

It didn't take many minutes to fix the mentioned above issue. I simply separated the 
`RemoveOrModifyQuestion(int numberOfQuestion, string modifiedQuestion = "")` method into a few more steps, so it
now looks like this:
```
    int numberOfQuestionToModify = Convert.ToInt32(VerifyAnswer(numberOfQuestions));
    string modifiedQuestion = CreateQuestion(true);

    if (modifiedQuestion != "")
    {
        fm.RemoveOrModifyQuestion(numberOfQuestionToModify, modifiedQuestion);
    }
    else
    {
        Console.WriteLine("Creation of new question failed. No change was made to the chosen question. Please try again.");
    }
```
It functions the exact same, but this version has an added failsafe to make sure that nothing goes wrong if you
fail to create a new question. I think the program as a whole is fully functional now, and I haven't found any 
unfixed bugs so far. I added ten base questions (I'm aware they aren't on GitHub, but they can be found 
commented out in previous versions in the Deck class) that can be removed or modified, which should suffice
for what this program is, I think. I'm not sure how file management would work should anyone download these files
from GitHub, but at it all works locally for me. So this base program should be done now, but I'm probably going 
to implement some API related features in a bit.

Bug spotted! It took me a grand total of two minutes to find a problem in my program. Got to be a new record, I think.
So anyhow, I chose to add a new MCSA question and added the same option twice. This, as indended, does not work. No
question was added, but a message saying it was, was still written out. It won't take long to fix.

Bug fixed, program work. I added a simple if statement to verify that when adding a question, the return value isn't
"". The only time it is equal to "" is when the question wasn't added, and so a message does not need to be written.
I only know of one way to fail the creation of a question, and that is to add the same option twice for MCSA questions.
For this scenario, I've already written a specific message, and as such, I believe no general message has to be
written for failing the creation of a question. Now it may be possible to input a value so large that the program
can't handle it and crashes, but I don't think I'll want to account for that possibility here. The indended
functions are functional, and I think that should suffice for now. I did test something along those lines though,
and when inputting a ridiculously large number (as many 9's as was allowed) for the "How many cards are in a 
regular card deck?" question, I got a message saying "Please input numbers, not letters". Odd result, but at least 
it didn't crash.

Quick fix later, I've added a .ToLower() in Quiz when checking the inputted answer to make sure that you still get 
a right answer if you for example type "iceland" when the answer is "Iceland". 
Makes it a little more user friendly, I think.