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
#### 4131 WPF Filhantering
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
```csharp
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

2023-11-24
--------------
#### 4131 WPF Filhantering
This morning, I thought of something. I thought: "What if you try to add a question that already exists in the quiz 
to the quiz?". Answer: it works. Some fifteen minutes later, now it doesn't. In the FileManager class, 
I added an if-statement to the AddQuestion method, like this:
```chsarp
if (questionCardString.ToLower().Contains(item[1].ToLower()))
{
    return false; //Quiz does not allow you to add a question that already exists.
}
```
I also changed the return type from void to bool, so that ActionHandler can check that if the return value is false,
write a message to the console stating that the question already exists. I also added trimming to the question and
answer you input for adding questions, so that no unnecessary spaces are included in the beginning or end of a 
question.
* https://learn.microsoft.com/en-us/dotnet/standard/base-types/trimming

2023-11-24
-------------
#### WPF Filhantering
It would seem that I have yet to separate the UI from the rest of the program to a sufficent degree. So I've started
fixing that, and I have only one class left to fix, I think. Problem is, it's the first or second class I wrote, so
at the time I wasn't thinking that I would eventually change the user interface. Alas, now I am. So, time to clean up
the Quiz class, I suppose. Thing is, it is BAD. REALLY bad. I think I'll actually just ditch most of the methods
in Quiz and move it all over to UserInterface instead, since so much of it is `Console.WriteLine` or `Console.ReadLine`.
I'll keep some simple calculations like how many answers you've gotten correct or something similar, but so much of
what's currently in Quiz does NOT belong there.

I'm not sure if I can even salvage this class. I'm thinking of simply starting over with the class Quiz, to get it 
right from the start. 

I'm also thinking of another thing. Namely, either straight up removing Quiz.cs and moving all its functions and 
methods to the class ActionHandler, or restructuring a few things a tad and treating Quiz like some sort of 
action handler of its own.

I'm going to try moving the instance creation of the class Quiz from ActionHandler to UserInterface. I'll also
largely adjust all the functions in Quiz, so that it acts more like ActionHandler does. In the end, it's probably
the smoothest fix I can think of.

2023-11-25
------------
#### WPF Filhantering
I'm getting an interesting error, here. When setting the return type to `List<QuestionCard>` for the Run() method in Quiz, 
I get an error stating `Inconsistent accessibility: return type 'List<QuestionCard>' is less accessible than method
Quiz.Run()`. I noticed that if I change the class from public back to internal, this error disappears. I'm unsure
how I should deal with this error, since the class may have to be public in order for other user interfaces (such as
the GUI that will be implemented soon) to have to have some form of access to the class Quiz.

Some two minutes later, the problem is no more. When I was testing how to fix it, I tried changing the access modifier
for another method in Quiz, one that returns a bool value. No error occured. It then took me about two seconds to 
realize that I can't return a list of QuestionCards from Quiz, because the class QuestionCard was still internal. So 
after switching QuestionCard from internal to public, it's working.
* https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/access-modifiers

Alright, quiz is back up and fully functional. The quiz is, functionally, the exact same as before, with one exception.
For questions such as "What is Eyjafjallajökull, and where is it located?", you can now answer something like "2".
In other words, the quiz doesn't stop you from inputting a single number for an answer that requires text.
There are benefits with the quiz stopping such answers, but there are also some benefits with this simpler approach 
as well, so I'll leave it as is for now. Anyhow, about 80% of what was in Quiz is now in ActionHandler, minus
all the unnecessary `Console.WriteLine()` and `Console.ReadLine()`. All those are now in UserInterface, so the 
program should be user interface-independent now. Since the user interface in usage has to call some methods in a
specific order for the quiz to work, I suppose it could've been done better. But I also suppose that is always the 
case. The quiz works, and now the UI is properly separated from the rest of the program. It took me a while to 
salvage the atrocity that was the Quiz class, but it is done.

2023-11-29
-------------
#### WPF Filhantering
I've been trying to implement a GUI for a bit now, but I was struggling with getting the project to start. I kept 
getting an error in XamlReader.cs (I haven't written this class). The Xaml wasn't able to parse, it seems. It took
me a while to find the cause of the error, but I've now found it. Adding `ActionHandler handler = new ActionHandler();`
to MainWindow.xaml.cs is the problem. Thing is, I don't know why this throws an error. I've tried running both 
the "FrågesportNetCore" project and the GUI project at the same time, but it still throws an error (I have a
`using FrågesportNetCore;` reference in the class that causes the error. This same error is thrown regardless of
where I instantiate the ActionHandler class, in the constructor or not. ActionHandler is also public. The error
is also thrown regardless of whether ActionHandler has a constructor or not.

Some testing later, I've made a little progress. I implemented a button that, when you press it, should write
instructions. It starts by instantiating ActionHandler. Then the program breaks. Same error as before, but on a 
different line and in EventRoute.cs instead of XamlReader.cs. It can't load FrågesportNetCore.exe because the 
.exe or .dll file is invalid. Same as before, even if I run both projects at the same time, trying to instantiate a 
class from FrågesportNetCore in WPF_GUI_Question instantly throws an error. At least I got the button working, so 
that's some measure of progress at least.

I'm not really getting anywhere with debugging this little problem, so I'll paste the details below for some reason:

```
System.BadImageFormatException
  HResult=0x800700C1
  Message=Could not load file or assembly 'C:\Users\04edfl12\OneDrive - Stenungsunds Kommun\Programmering\FrågesportNetCore\WPF_GUI_Question\bin\Debug\net8.0-windows\FrågesportNetCore.exe'. Format of the executable (.exe) or library (.dll) is invalid.
  Source=WPF_GUI_Question
  StackTrace:
   at WPF_GUI_Question.MainWindow.Btn1_Click(Object sender, RoutedEventArgs e) in C:\Users\04edfl12\OneDrive - Stenungsunds Kommun\Programmering\FrågesportNetCore\WPF_GUI_Question\MainWindow.xaml.cs:line 32
   at System.Windows.EventRoute.InvokeHandlersImpl(Object source, RoutedEventArgs args, Boolean reRaised) in System.Windows\EventRoute.cs:line 123
   at System.Windows.UIElement.RaiseEventImpl(DependencyObject sender, RoutedEventArgs args) in System.Windows\UIElement.cs:line 6297
   at System.Windows.Controls.Primitives.ButtonBase.OnClick()
   at System.Windows.Controls.Button.OnClick()
   at System.Windows.Controls.Primitives.ButtonBase.OnMouseLeftButtonUp(MouseButtonEventArgs e)
   at System.Windows.RoutedEventArgs.InvokeHandler(Delegate handler, Object target) in System.Windows\RoutedEventArgs.cs:line 201
   at System.Windows.EventRoute.InvokeHandlersImpl(Object source, RoutedEventArgs args, Boolean reRaised) in System.Windows\EventRoute.cs:line 123
   at System.Windows.UIElement.ReRaiseEventAs(DependencyObject sender, RoutedEventArgs args, RoutedEvent newEvent) in System.Windows\UIElement.cs:line 6272
   at System.Windows.RoutedEventArgs.InvokeHandler(Delegate handler, Object target) in System.Windows\RoutedEventArgs.cs:line 201
   at System.Windows.EventRoute.InvokeHandlersImpl(Object source, RoutedEventArgs args, Boolean reRaised) in System.Windows\EventRoute.cs:line 123
   at System.Windows.UIElement.RaiseEventImpl(DependencyObject sender, RoutedEventArgs args) in System.Windows\UIElement.cs:line 6297
   at System.Windows.UIElement.RaiseTrustedEvent(RoutedEventArgs args) in System.Windows\UIElement.cs:line 2710
   at System.Windows.Input.InputManager.ProcessStagingArea() in System.Windows.Input\InputManager.cs:line 546
   at System.Windows.Input.InputProviderSite.ReportInput(InputReport inputReport) in System.Windows.Input\InputProviderSite.cs:line 52
   at System.Windows.Interop.HwndMouseInputProvider.ReportInput(IntPtr hwnd, InputMode mode, Int32 timestamp, RawMouseActions actions, Int32 x, Int32 y, Int32 wheel) in System.Windows.Interop\HwndMouseInputProvider.cs:line 797
   at System.Windows.Interop.HwndMouseInputProvider.FilterMessage(IntPtr hwnd, WindowMessage msg, IntPtr wParam, IntPtr lParam, Boolean& handled) in System.Windows.Interop\HwndMouseInputProvider.cs:line 350
   at System.Windows.Interop.HwndSource.InputFilterMessage(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, Boolean& handled) in System.Windows.Interop\HwndSource.cs:line 1140
   at MS.Win32.HwndWrapper.WndProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, Boolean& handled)
   at MS.Win32.HwndSubclass.DispatcherCallbackOperation(Object o)
   at System.Windows.Threading.ExceptionWrapper.InternalRealCall(Delegate callback, Object args, Int32 numArgs)
   at System.Windows.Threading.ExceptionWrapper.TryCatchWhen(Object source, Delegate callback, Object args, Int32 numArgs, Delegate catchHandler)
   at System.Windows.Threading.Dispatcher.LegacyInvokeImpl(DispatcherPriority priority, TimeSpan timeout, Delegate method, Object args, Int32 numArgs)
   at MS.Win32.HwndSubclass.SubclassWndProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam)
   at MS.Win32.UnsafeNativeMethods.DispatchMessage(MSG& msg)
   at System.Windows.Threading.Dispatcher.PushFrameImpl(DispatcherFrame frame)
   at System.Windows.Application.RunDispatcher(Object ignore)
   at System.Windows.Application.RunInternal(Window window)
   at WPF_GUI_Question.App.Main()
```

Lot of details in this one, I think. I came across an answer on StackOverflow 
(https://stackoverflow.com/questions/323140/system-badimageformatexception-could-not-load-file-or-assembly-from-installuti)
suggesting to change the processor architecture for AnyCPU projects from auto. Alas, this did not fix my problem.

2023-12-01
--------------
#### WPF Filhantering
Right so the UI was not properly separated from the rest of the program. But now it is! I think. The console UI is now
located in a new project, "FrågesportNetFramework", which is essentially a copy of "FrågesportNetCore". I removed
the latter, since it has been replaced. Kind of funny, in a way, how this project as a whole has changed. Anyhow
I had a lot of trouble fixing dependencies and stuff with the three projects, but it works now. It seems that 
"FrågesportNetCore" targeting .NET framework version 4.8 was not very good for creating dependencies with projects that
target .NET framework version 8. So "FrågesportNetFramework" targets .NET framework version 8, and now it all works.
I'll continue working on the GUI, which can access all functions and classes of the new project QuizLibrary, which is
where all the classes that the user doesn't directly interact with are located.
* https://stackoverflow.com/questions/38042130/what-is-a-circular-dependency-and-how-can-i-solve-it
* https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/cs0246?f1url=%3FappId%3Droslyn%26k%3Dk(CS0246)
* https://stackoverflow.com/questions/48555467/cant-add-reference-to-visual-c-sharp-console-app-in-visual-studio
* https://stackoverflow.com/questions/38148128/how-do-i-reference-a-net-framework-project-in-a-net-core-project

I'm not sure why I keep thinking I've properly separated the UI-related classes from the rest, to a  SUFFICIENT degree. 
Clearly I haven't, yet. I was about to implement the whole "choose your option" in the GUI, and I'm only now realizing 
how far from adequate the UserInterface class is. I should probably tacke it immediately to make my life a little
simpler. And I should probably stop thinking I've ever completely separated the UI from the rest of the project. At 
this point, it's getting somewhat ridiculous how many times I've said I've completed it only to realize how incorrect
I've been *every time*...

2023-12-05
-------------
#### WPF Filhantering
With the quiz.cs class, I feel like I sort of shot myself in the foot with the poor separation of UI and such. 
UserInterface, though, this is something else. I hardly know what it should look like with a proper separation, so I 
don't know if I'm even doing it right. I *think* it's getting a little better, maybe? It's taking far longer than it
has any right to, though. Henceforth, I will be sure never to make these mistakes again, as it is an atrocious process 
to separate an existing class into part-UI, and part-not-UI like this.

I've successfully got the quiz working, but I'm having issues with making question creation efficient. I'm struggling
with separating it in a good way, since there needs to be various numbers of `Console.ReadLine()` based on what you
answer. This leads to issues when I need to call certain methods in a certain order a certain amount of times. My
main idea is to return the name of a method (or maybe just the method itself) from InterfaceHandler to UserInterface
to make it easier to call the correct methods in the correct order after each `Console.ReadLine()`. I don't know how
to do this, though. I don't even know if it's possible (or good practice for that matter). I've researched 
delegates and asynchronous methods, but I'm unsure of whether one of these will actually provide what I need.
* https://stackoverflow.com/questions/25191512/async-await-return-task
* https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/await
* https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/how-to-combine-delegates-multicast-delegates

2023-12-06
--------------
#### WPF Filhantering
I'm growing more certain that I, with my current skill, am unable to *fully* separate the UI from the rest. I'm making
progress, sure, but not enough. I guess I'll have to accept an inadequate separation in this case, but I will do my
best at separating them still. From what I can tell so far, UserInterface is looking to be around half of it's 
previous length in code, so it's not like I've done nothing I suppose.

2023-12-08
-----------------
#### WPF Filhantering
I've continued working on UserInterface. I'm almost done with it, but then I realized that the MCSA questions are broken.
Again. The options aren't displaying, but I'll fix it in due time. It probably won't take too long, I think. I've also
made some progress with adding options for removing/modifying questions, and it's going well enough.

Update: I don't know what I'm doing. Somewhere, sometime, I changed something. Not a great combination of events. So 
anyhow the questions aren't randomized, ever. I have a method, Draw(), in Deck, for this. I must've used it at some
point, since I recall the questions being randomized before I started trying to fix the atrocity that was UserInterface.
So I also need to fix the questions not being randomized. Not much fun in seeing them in the same order all the time,
I think.

2023-12-09
--------------
#### WPF Filhantering
I've fixed the questions not being randomized. It didn't take that long, but it was an interesting path I had to 
take to get there. It showed that this project is getting a little complex, maybe too complex for what it is. Anyhow,
this is the path I followed: 1. UserInterface calls InterfaceHandler.PerformAction(). 2. InterfaceHandler calls 
ActionHandler.PrepareQuiz. 3. ActionHandler calls Quiz.Run. Quiz creates a new instance of Deck, randomizes
all the cards and returns them as a List of QuestionCards. This was the first part, where I saw how I was randomizing
the questions. Second part: 1. UserInterface calls InterfaceHandler.PerformAction(). InterfaceHandler calls
ActionHandler.GetAllQuestions(). And here the problem was. I'm not sure why or when, but at some point when implementing
the method GetAllQuestions(), I chose to get the questions from FileManager.ReadFile() instead of from the *already 
existing list containing all the questions*. I really don't know what I was thinking there. Anyhow, since the 
randomization of questions happens in Quiz, getting the questions from FileManager.ReadFile() will cause them to be
in the same order as they are in the .txt document, every time. Quick fix later, the questions are now being randomized.

I've also found the reason for the MCSA questions not displaying their answers. Similar to the previous question, I
did something at one point and seem to have forgotten about it later. As it is right now, when taking the quiz,
all the questions get added to a list, twice. So if there are ten questions, the list will contain twenty questions.
The MCSA questions added by one method do contain options, while they don't in the other.

Two minutes later, problem solved. The debugger really is quite helpful. So anyway what I had to do was move the
calling of the RunQuiz() method in InterfaceHandler, and clear listOfAllQuestions instead of setting it to 
ActionHandler.GetAllQuestions().

Now that I'm working on targeting questions currently in the quiz, I realized that the questions not being randomized
above was actually not the problem, the problem was simply how I was adding them to a list of questions. So I changed
some of that, and it's now possible to get the questions as randomized or not. This will make it a little simpler
to target questions when you want to remove or modify them.

At last, the console user interface is now working again. It took a gruesome amount of time to repair it all after this
brutal separation, but I dare not assume that it is wholly separated just yet. At least it's better now, I think. 
UserInterface and InterfaceHandler include over 50% more code than what UserInterface had before, but hopefully it'll save
me a lot of time and effort when I implement the GUI, which I will continue doing tomorrow. There was a lot of debugging
involved in this whole part, and doing it without the debugger would have been an absolute nightmare. Being able to step
into methods and follow variable assignment and operations was certainly a saving grace here.
* https://stackoverflow.com/questions/7745509/how-to-specify-default-value-for-list-parameter-in-a-c-sharp-function
* https://stackoverflow.com/questions/4630444/how-to-skip-optional-parameters-in-c

2023-12-12
-------------
#### WPF Filhantering
At first, I was trying to fix some things with MCSA questions in the GUI. I was trying to use patterns and Regex and a
bunch of stuff that was unnecessary. I then discussed the project with my teacher, and came to the conclusion that I've 
been handling some things incorrectly. We set up a basic structure, and I think it's now time to actually get this
project going. I've been handling the QuestionCard and MCSACard classes wrong, since when for example comparing the 
inputted answer with the correct answer, I haven't accessed the card's actual correct answer but rather a copy of
that value. Some unnecessary things, in other words. I will clean up all of that, then fix the GUI (which will be simpler
with these fixes) and then implement more features. Just commenting out two object variables that did not adhere to
these principles brought about 10 errors. It's time to actually get this project fixed, definitively.

Fascinating, how much I fixed by only removing code. All I did was move code, remove code, and combine InterfaceHandler
with ActionHandler. And now the program works a lot better. The command-line UI still works great (I only had to 
adjust some minor things to make it work), and the implementation of the GUI is going well, too. Before I changed
the things I mentioned above, I was struggling with displaying each option for MCSA questions on a different button. 
I was trying to use patterns and regexes and it was *not* going well. But after the changes, it took me a mere minute
to get it working perfectly. I suppose this will be a lesson always to keep a clear idea of what I am doing, and not
implement a bunch of useless workarounds for no obvious reason.

When checking the answer that the user inputs in the GUI, I ran into a problem with checking the MCSA answer. The 
regular questions where you type in your answer worked fine, but the MCSA ones did not. It took me a minute or two to
realize, but when I was debugging the project I realized that sending the `[button].Content.ToString()` sends the 
content of the button (obviously), while InterfaceHandler expects an answer in the range 1-5. No major issue, I simply
changed `[button].Content.ToString()` to `"[number of the button in question]"`.

2023-12-13
------------
#### WPF Filhantering
I've run into a curious issue. When checking the answer in the GUI, I always get an incorrect. Using the debugger, I 
quickly saw that the question was comparing with a different question than is displayed. It was working earlier, so 
I must've changed something recently. I know for a fact it has to do with indexes, I just need to find where.

As expected, 1) Solving the issue took about 20 seconds, and 2) It had to do with indexes. The issue was simply that I
moved the incrementation of an index up, instead of down, in the code. So the quiz can now be played in a GUI as well,
it is fully functional and can be replayed both in a command-line UI and a GUI now. I of course have to add the remaining
four actions to the GUI as well, some of which will be a little more troublesome than others. Like a true professional,
I copy pasted the first reply from StackOverflow regarding one of these actions. In fairness, the code was literally
just `Application.Current.Shutdown();`. 
* https://stackoverflow.com/questions/2820357/how-do-i-exit-a-wpf-application-programmatically

Problem spotted! Not sure where it came from this time. In the command-line UI, when I want to choose a question to 
target, for removal or modification, the questions come in a randomized order. However, if I target question number 3
(they are lined up with a number, 1-cards.Count), the program targets the third question in the file, which is the
third question when they are *not* randomized. I know what the issue is, I just have to think of a nice way to solve it.

The problem was a little more complex than I was expecting, but I think I know what to do now. The user picks a question
to target, from a randomized list. The number of the question is then used to target a question. I was trying to 
convert this number of the question from the randomized list to the number of the same question in the sorted list. 
The problem I was trying to fix is that I couldn't get IndexOf to work. My code looked like this: 
`numberOfQuestion = quiz.ListOfSortedQuestionCards().IndexOf(questionCards[Convert.ToInt32(numberOfQuestion)]).ToString();`.
It took me a while to realize (I think I know what is wrong anyway), but the problem is that I'm comparing two different
instances of the class QuestionCard. They are identical, but they are still different instances of the class. I assume
this is why it doesn't work.

Bingo! Comparing the questions of the questions instead of the questions themselves (questionCard.Question instead of just
questionCard) did the trick.

2023-12-14
----------------
#### WPF Filhantering
It wasn't quite as easy as it seemed at first, to fix the issue I ran into yesterday. But now it is fixed, so there's 
that. I changed the code to: 
```csharp
foreach (QuestionCard item in quiz.ListOfSortedQuestionCards())
    {
        if (item.Question == questionCards[Convert.ToInt32(numberOfQuestion)].Question)
        {                    
            numberOfQuestion = quiz.ListOfSortedQuestionCards().IndexOf(item).ToString();
            break;
        }
    }
```
Not too complicated, but it works. Back to the GUI now.

2023-12-17
------------
#### WPF Filhantering
Since last time, I've created a React projcet with a frontend client and backend server. I haven't done anything
in the actual project, but I've been learning some React from a classmate. Anyhow, I've also finished the GUI now. 
From what i can tell, all the options work just fine. I can take the quiz, add a question, remove a question,
modify a question and close the application. All of these appear to work before and after each option has been tested.
There are probably some bugs hidden in the depths somewhere, but it looks to be working anyway. I mostly have small
methods that set booleans to false or something of the like, but there are also two button click methods that are 
kind of massive. I would've preferred to keep it more concise, but I couldn't find many better solutions at present 
time. It was troublesome, to say the least, to deal with all possible scenarios that could take place when someone
presses buttons in a certain order. But, as I said earlier, it looks like it's working, so I'll move on to the React
frontend and API backend now.

2023-12-19
---------------
#### WPF Filhantering
I'd already created a frontend and a backend project using a template in Visual Studio, so here I thought testing the
React app would be easy. It wasn't. I'm unsure of whether I had a bit of tunnel vision or if the template created 
something that differed slightly from what I required, but it did *not* go well. I ran into a lot of errors, debugged,
and when I finally got the project running, it was not quite what I wanted. So I restarted, but by creating only a 
React application similar to the React documentation. I then copied that test project into this one, and it works. 
I can't see it in Visual Studio, which I'm almost certain is because the project contains no .csproj file and so the
.sln has no reference to it, but that's fine. I'm developing the React frontend using VSCodium anyway, and using Git
through PowerShell provides more practice than doing it through Visual Studio. So the project should now be ready for
development. I also created an interface class for FileManager and the new DatabaseManager, so InterfaceHandler
should now be independent of the manager. This means that, in theory, I should not have do adjust InterfaceHandler
at all when changing between reading from/writing to a file or a database.

2023-12-20
---------------
#### WPF Filhantering
Come to think of it, perhaps I should stop stating that I'm still working with "file management". It probably doesn't
matter too much anyway though, since I don't think anyone will read this wall of text that is my journal for this project.
Anyway I've started the API a couple of times now, and I've finally accomplished my first goal: getting rid of Swagger.
Not that it's bad, but I prefer Postman (also it took me like 5 minutes to stop Swagger and some SPA thing to launch, 
it was no big task). It gives cleaner and clearer results and is easier to use, I think. Time to start adjusting
this template API to make it more fit for delivering the data I need, hm.

A few hours later, I've yet to start adjusting this template API to make it more fit for delivering the data I need, hm.
Instead, I downloaded SSMS, SQL Express and set up a database with fitting tables. It was a little troublesome, but I
think I've got it working now. Before starting work with the API, I should probably insert the initial questions though.

Looks like the database is ready to go, I think. I've added all starter questions to tables, where there is one table
containing all questions, one table for type QuestionCard questions, and one table for type MCSACard questions.

I've been working for a bit with the API, and it's been going OK. I had to change some files, rename some files, move 
some files and create some files, but I've got the template properly set up now, I think. When starting the API, I can
get a 200 OK message by sending an HTTP GET request to "https://localhost:7140/api/quiz" with Postman. So far, so good.

2023-12-25
-----------
I've continued working on the API, as mentioned above. I've added models, a context, modified the controller and such.
I struggled *a lot* with a specific error telling me something about not changing invariant globalization or 
something. I eventually fixed it by changing `<InvariantGlobalization>true</InvariantGlobalization>` to
`<InvariantGlobalization>false</InvariantGlobalization>` in Web_App.Server.csproj, but it took me quite a while to find
that. And now I'm instead trying to fix this error message:
```
Internal Server Error: Microsoft.Data.SqlClient.SqlException (0x80131904): A connection was successfully established with the server, but then an error occurred during the login process. (provider: SSL Provider, error: 0 - Certifikatkedjan utfärdades av en icke betrodd certifikatutfärdare.)
```

It's taken me far longer than I would've liked, and I've yet to get it fixed. Did I mention all of this is just to
connect to the SQL Server?

It looks like the connection is working now? I don't even feel like I accomplished anything; it took me probably around
3-4 hours just to connect to the SQL Server through my API. If not more, I don't really know. I was doing this for 
at least an hour and a half yesterday as well, but at least this part's over at last.

Hours upon hours. What little progress I make is hardly relevant. I can get all the questions, their ID, and their type
from the Questions table in the Quiz database, but that's it. No matter how much I try, or what I try, I can't seem
to figure out how to access the other tables (QuestionCard and MCSACard. These contain the correct answer and potential
options). A query such as this works: 
```csharp
var allQuestions = quizContext.Questions
    .Select(q => new
    {
        q.QuestionId,
        q.QuestionText,
        q.QuestionType
    })
    .ToList();
```
...but this doesnt: `var sampleQuestion = quizContext.Questions.ToList();`. Not even this does: 
`var sampleQuestion = quizContext.QuestionCards.ToList();`. Take the latter, for example. The exact reason why it 
doesn't work is because the query searches for properties found in the QuestionCard model, RequiredWords, in 
the Questions table. I can't figure out how to search for the property in the QuestionCard table instead. I've
searched the web for solutions, asked ChatGPT, but all to no avail. 

Looks like everything's working now, at last. Seems I had a tad too much tunnel vision, trying a little too 
rigorously to follow ChatGPT's advice. It adviced me to use a discriminator column and such for structure,
looking a bit like this:
```csharp
    modelBuilder.Entity<Question>()
        .HasDiscriminator<string>("QuestionType")  // Discriminator column for TPH
        .HasValue<QuestionCard>("QuestionCard")    // Discriminator value for QuestionCard
        .HasValue<MCSACard>("MCSACard");           // Discriminator value for MCSACard

        // Additional configuration for QuestionCard
    modelBuilder.Entity<QuestionCard>()
        .HasBaseType<Question>()
        .HasKey(qc => qc.QuestionId);

    // Additional configuration for MCSACard
    modelBuilder.Entity<MCSACard>()
        .HasBaseType<Question>()
        .HasKey(mc => mc.QuestionId);
```
However, I could not get this to work no matter how much I tried. Eventually, I gave up trying so strictly to 
follow ChatGPT's instructions. Instead, I wrote this myself:
```csharp
    modelBuilder.Entity<Question>()
        .HasKey(q => q.QuestionId);

    modelBuilder.Entity<Question>()
        .ToTable("Questions");

    //Configurations for QuestionCard
    modelBuilder.Entity<QuestionCard>()
        .ToTable("QuestionCard");

    //Configurations for MCSACard
    modelBuilder.Entity<MCSACard>()
        .ToTable("MCSACard");
```
And it worked perfectly, from what I can tell. Maybe it's poorly written, maybe not. I don't really know, but I do
know that it works. I can get all the questions with the following query: `var q = quizContext.Questions.ToList();`.
I can also get only QuestionCard type questions by changing `Questions` to `QuestionCards`, or only MCSA type questions
by changing `Questions` to `MCSACards`. At any rate, this is what one of the questions looks like as a JSON object
when requesting all questions with `var q = quizContext.Questions.ToList();`:
```csharp
{
    "questionId": 1,
    "questionText": "In chess, how many pawns do each side start with?",
    "questionType": "MCSACard"
}
```
Now that this is all fixed, I should be able to tailor it to meet the specific
needs of the frontend part of this application. Actually come to think of it, I'll have to fix the service class and
make sure QuizController uses Service.cs for interacting with the database. By the way, adding 
`.LogTo(Console.WriteLine, LogLevel.Information)` to 
`options.UseSqlServer(Environment.GetEnvironmentVariable("QuizConnection"))` in Startup.cs helped with finding 
problems and debugging. (Also, that environment variable was criminally annoying to fix.)

This has all been a lesson on many levels, but I'm mainly just glad to have this part finished.
I'd rather not spend next Christmas trying not to lose my mind over APIs and SQL servers/databases/tables. 
I need some rest now, enough REST.
* https://learn.microsoft.com/en-us/sql/relational-databases/databases/create-a-database?view=sql-server-ver16
* https://www.youtube.com/watch?v=PPFyoXA_FC0
* https://www.microsoft.com/en-us/sql-server/sql-server-downloads
* https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16
* https://stackoverflow.com/questions/76394279/scaffold-dbcontext-culturenotfoundexception-only-the-invariant-culture-is-sup
* https://stackoverflow.com/questions/17650482/instance-failure-error-while-connection-string-is-correct
* https://stackoverflow.com/questions/70399243/how-to-fix-sql-server-2019-connection-error-due-to-certificate-issue

2023-12-26
--------------
As explained above, the API is working, but I've now gotten very unsure of how to continue. I obviously want to reuse
the code I wrote in QuizLibrary for efficiency, but I'm not sure how to incorporate it into the API. Because of the
way the API is reading from the SQL database, I don't think the methods would look the same in DatabaseManager 
(or QuizContext, which is the class that is currently interacting with the database) as FileManager. Perhaps this
is more a question of structure, what part does the API play in this project? I suspect that if I don't set this in 
stone before proceeding, I'm probably shooting myself in the foot again. And then I won't have any other of my feet to 
shoot. So I'd rather not do that. So I'll document some of my thoughts now, to see if that'll get my anywhere.

So, for starters, I want this project to be efficient. It can save me a lot of time in the future if I do it right 
from the start, which is something I did *not* do the first time around when initially creating this project. So, 
InterfaceHandler has to be used for basically everything. Which means that the React application should use the API to
interact with InterfaceHandler, unless I am completely mistaken. Which means that I am very unsure of how I should 
continue, so I might just have to wait until school starts again to ask my teacher. I don't want to do something
stupid and suffer through salvaging the whole thing like I did with the GUI and InterfaceHandler.

2024-01-09
-----------
The React project should communicate with the API using links such as 'https://localhost:7140/api/quiz/1', and the API
communicates with InterfaceHandler to return a bunch of stuff. The path is, as of right now, clear. I've run into an
interesting problrm, however. Currently, if I request all the questions, I get them all with a questionID, the type of
question (QuestionCard or MCSACard) and the question itself. This works well, but if I try to get a single question, 
for example with the code `var question = quizContext.Questions.FirstOrDefault(q => q.QuestionId == id);`, I get the
questionID, questionType, questionText and requiredWords (this is for QuestionCard only. MCSACard gives similar 
results though). The interesting part is that I'm querying the Questions table, and the results returned are from BOTH
the Questions table and the QuestionCard table. This doesn't have to be an issue, though. I can probably turn it around
and use it to my advantage, I'll just need to tinker with it for a bit.

I'm a little unsure of how to fix a problem I'm currently stuck at. I need to prevent the correct answer from being 
returned as I explained above, but currently I can not access the properties of MCSACard or QuestionCard.

Problem fixed.

```chsarp
var question = quizContext.QuestionCards.FirstOrDefault(q => q.QuestionId == id) as QuestionModel
    ?? quizContext.MCSACards.FirstOrDefault(q => q.QuestionId == id);

if (question is QuestionCardModel questionCard)
{
    questionCard.RequiredWords = null;
}
else if (question is MCSACardModel mcsaCard)
{
    mcsaCard.CorrectOptionNumber = 0;
}
```

It works as intended. The code above removes the answer from the requested question. Well it doesnt *remove* the question, 
it sets it to null or zero. Either way you can't cheat by finding the real answer in the debugger, which was the point.

The API now accepts more arguments. 'api/quiz' returns a list of all questions, 'api/quiz/4' returns the question
with questionId 4 (without answer), 'api/quiz/[anything unknown]' returns a StatusCode NotFound with a 'Resource not
found' message, and 'api/quiz/instructions' returns a list of all initial instructions. This is, in other words, 
good progress towards a functional controller class. At least I think it is. I don't really know.

Now that I've arrived at the part where the API returns instructions regarding how the quiz is played, I've taken
notice of something in InterfaceHandler. It's definetely not customized for API usage. Like this, it seems I may have
to use the class more sparingly than I'd hoped, but I suppose it makes sense since it'd otherwise have to be customized
for usage not only by a command-line application, but also a WPF GUI application as well as a web-based one. Since I'm 
no god of C# and do not possess an otherwordly form of foresight, I did not expect to need access to InterfaceHandler
without using any manager in the process. Many of InterfaceHandler's methods rely on usage of a manager, such as 
FileManager, which I will not be using in this case. The API essentially already deals with any tasks that 
DatabaseManager would handle. Speaking of, I should probably remove that class.

As stated above, InterfaceHandler is not customized for API usage. I suspect this is going to become more of a problem
as I move on. Right now, I'm pondering the possibilities regarding checking the answer that a player has submitted.
This is the method in InterfaceHandler that checks your answer: 
```csharp
private string CheckQuestionAnswer(string answer)
{
    QuestionCard card = questionCards[totalAnswers++];

    int pointsGained = card.CheckQuestionAnswer(answer);

    correctAnswers += pointsGained;

    if (pointsGained == 0)
    {
        return "Incorrect.";
    }
    return "Correct!";
}
```
This is a problem. The current question is determined by the total number of answers, but from an array of all questions.
These questions are read from the file using FileManager. So, I'm going to have to fix this somehow. I know for a fact
that there are many solutions to this problem, and I'm going to have to find a suitable one to make sure I don't shoot
myself in the foot again. Again.

2024-1-12
-------------
With some help and guidance, I've gotten a suggestion for a new approach that could potentially make this whole project
very smooth indeed. Instead of struggling my way through using InterfaceHandler with the API somehow, I can do it 
the other way around. If I ditch the old file managing system of using a file, I can use the API and SQL database 
for every project, command-line, desktop and React application. If I manage it well, it could make the project more 
concise, faster, and more scalable.

2024-1-16
----------
After much trial and error and more errors, I have successfully made an HTTP GET request from InterfaceHandler. However,
I have a few classes execute. I'll try to adjust this project as a whole to use only the API and SQL database. No
file manager, in other words. Several of the classes in QuizLibrary will, as such, become obsolete. These include
MCSACard.cs, QuestionCard.cs, IManager.cs, and Filemanager.cs. Possibly also Deck.cs and Quiz.cs. Long story short,
every single class except InterfaceHandler.cs may soon depart once more. Doing so will make the API rather chunky though,
so I may want to take that into account. Come to think of it, Deck.cs is basically already obsolete. Using the API, I
don't even need any deck. Interesting. Plan seems to be to move everything from InterfaceHandler to the API, but that
would mean many lines of code.
* https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient
* https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/deserialization
* https://learn.microsoft.com/en-us/dotnet/standard/parallel-programming/how-to-return-a-value-from-a-task

2024-01-19
-----------
MediatR works now - it doesn't crash the program anymore.
* https://stackoverflow.com/questions/75527541/could-not-load-type-mediatr-servicefactory

I'm pondering possibilities regarding question storage. A short discussion later, it's decided that I will add a new 
table to the SQL database. It will contain four rows:
1. (`string`) Name of current player
2. (`int`) Number of correct answers
3. (`int`) Number of current question
4. (`List<int>`) List of all QuestionIds of questions in the current quiz session

This table should help tremendously both in keeping track of relevant information and in separating frontend from 
backend. Should I do it right, the player will not be able to cheat so easily.

I now have an SQL query with which I've created the table.

```sql
CREATE TABLE dbo.PlayerStatistics (
    PlayerName NVARCHAR(30) NOT NULL,
    CorrectAnswers INT NOT NULL,
    NumberOfCurrentQuestion INT NOT NULL,
    ListOfQuestionIds NVARCHAR(MAX) NOT NULL, 

    CONSTRAINT PK_PlayerStatistics PRIMARY KEY (PlayerName),
    CONSTRAINT FK_PlayerStatistics_Questions FOREIGN KEY (ListOfQuestionIds)
        REFERENCES dbo.Questions(QuestionId)
);
```

Tried to, anyway. This query has ListOfQuestionIds, which was to be a comma-separated string containing QuestionIds of 
all current questions. Alas, since QuestionIds is an integer in dbo.Questions, the query caused an error. It looks 
like I either need to change ListOfQuestionIds from a string, or add a whole separate table. I'll see how I can fix
the issue.

Problem removed, literally. Since a foreign key from ListOfQuestionIds to dbo.Questions(QuestionId) isn't even necessary,
I simply removed the code from the query. And just like that, it works. It looks good, but I'll have to test it to 
make sure it works. Also, the interactions with this table are going to be a little more detailed than those
with the other tables.

I've now implemented and fixed any issues regarding a new PlayerStatisticsModel that maps to the PlayerStatistics table
in the SQL database. All works well, as far as I can tell.

Now, one can initialize the quiz with a new or existing player name. The player name is added to the new table
PlayerStatistics in the SQL database, along some randomized QuestionIds.

2024-01-23
-----------
After a bit of struggle, but honestly surprisingly little, I've now implemented usage of MediatR. Instead of 
QuizController interacting directly with QuizService, it now uses MediatR to send a command using 
a command handler which uses a command. Currently, this only works for the InitializeQuiz method, and I've three errors
regarding the name 'quizService' not existing in the current context. However, this should be easy to fix as I shall
modify each method to use MediatR as I just did with InitializeQuiz. I've also tested the method in question and it 
appears to work just fine.

Some time later, I've now fully integrated usage of MediatR into the project. Now, every existing method in QuizController
uses MediatR queries and commands to communicate with QuizService. I created a Handlers folder, containing a Questions
folder and a Quiz folder. These contain Queries, QueryHandlers, Commands, and CommandHandlers.

2024-01-26
-----------
I've now updated the MediatR usage. I combined the Query and QueryHandlers into one, same with Command and CommandHandler.
I also added a success body class to each query and command. It makes the code cleaner, clearer, and more concise.
Also, thanks to the added response body class, I can return both a bool 'Success' and a string 'ErrorMessage', so in
QuizController, I can check that 'Success' is true. If it is, return Ok(), if not, return StatusCode(500, ErrorMessage).
Improvements, so to say.

Now, one can also check their answer. Not in any of the programs, but through Postman I can check an answer. The following:
'https://localhost:7140/api/quiz/John Doe/2/iceland volcano' will return an Ok('Correct!'). The question is 'What is 
Eyjafjallajökull, and where is it located?'. I almost made a dummy mistake that caused it to return 'Incorrect' 
instead, and here's why:
```csharp
if (question.Question.CheckQuestionAnswer(playerAnswer) != 0)
{
    return Ok("Correct!");
}
```
This is correct, and it uses the method in QuestionModel to check the player's answer. As it should be, in other words.
The following is what I wrote at first, before realizing it was incorect: 
```chsarp
if (playerAnswer == question.Question.GetCorrectAnswer())
{
    return Ok("Correct!");
}
```
This checks that the player answer is identical to the question's correct answer. It doesn't have to be wrong - it gets
the job done. But it wouldn't have used a method I made long ago for this very reason. Answering 
'It is a volcano on Iceland' should return correct even if, in the database, the correct answer is stored as 
'iceland volcano'. The method I mentioned that checks the answer makes sure that the words stored in the database
are included in the player's correct answer, it doesn't check that they're identical. Simply put, this way of doing it
allows for a little more freedom in the player's answers.

Now, one can get a question, without or with an answer (the latter is not accessible unless the player also sends
their guessed answer, so I don't *think* you can cheat using that method), and also check if the inputted answer is
correct or not. What's next is to fix 'NumberOfCurrentQuestion' in the table in the SQL database. It needs to be set to
an initial value at some point, to then be updated at another point. This is number I will use to get each question, so
it's obviously important for the continued evolution of this project.

Sidenote: I also added `throw new Exception();` to test that the returned ErrorMessage from the MediatR queries and 
commands works properly, and it does. I then removed the `throw new Exception();`, of course.

* https://stackoverflow.com/questions/75527541/could-not-load-type-mediatr-servicefactory
* https://stackoverflow.com/questions/12886559/cannot-implicitly-convert-type-from-task
* https://stackoverflow.com/questions/7465704/how-i-can-convert-string-to-listint

2024-01-30
------------
I've now added some code to the InitializeQuiz method in QuizService.cs that splits the space separated list into a 
List<int>. It gave me a bit of trouble, since an error was being thrown. I did not notice this at first though, since
my try-catch statements appear to be a little off. If an error is thrown in QuizService.cs, a bool false is returned.
In InitializeQuizcommand.cs, the property QuizInitializedSuccessfully will be set to false. This does not, however,
change anything relevant, as Success is set to true and the rest of the program assumes the quiz initialization was 
successful. As such, I was getting a 'Quiz has been initialized successfully' message, which confused me. It did not take
me too long to realize what the issue was, and it turns out that a trailing space after the last number in the space
separated list of QuestionIds (string) caused an error to be thrown. So, I added .Trim() and it worked. I'll have to
fix the try-catch statements though, they are not fine as is. On the bright side, the player is now given a current
question number upon initializing the quiz.

I've split the GetQuestionWithOrWithoutAnswer (or whatever it was called) in QuizService into three methods now. It
makes the code more clear, simply put. This means that I also added another MediatR command, so now there's one for
retrieving a question and one for checking an answer and retrieving a response as a string based on the answer.

2024-02-02
-----------
There are a couple of warnings that I'm looking at. They bother me. Warnings are warnings - no good. I'd prefer to get
rid of them, and I know of a way, adding '?' to the data type. I'm not sure if this is good practice or not, though. 
It might make the program less efficient, I'll have to look into it a bit more before deciding on any approach.

I've fixed most of the warnings, but two remain. One I've decided to ignore after some research and thought, and the
other I will fix in time. Since it involves interaction with the database - the warning is regarding a nullable value
that is found in the SQL database, it might cause some database errors. I don't really know, but I'll be wary of it.

When testing the quiz, I came across an interesting problem. Getting each question and checking a correct answer
works properly, but I ran into another issue as well. This one was a quick fix - all I needed to do was move the code
`quizContext.SaveChanges();` so that it runs regardless of whether the player just finished the quiz or not. The other
problem, on the other hand, seems a little more interesting. So, when the player has finished the quiz, they should no
longer be able to retrieve any questions. However, to avoid errors, I do not increment 'numberOfCurrentQuestion' when
the player finishes the quiz. This, in turn, means that any repeated attempts at getting a question after finishing
the quiz will always return the last question.

Now, the errors above and some others are fixed. Now, when the player finishes the quiz, they cannot retrieve a question.
Instead, they will be asked to initiate the quiz in order to play again. Also, I made it so that the player's final
score is returned on each consecutive attempt at retrieving a question (their final result is also returned upon 
quiz completion). Some testing later, all seems to be in order.

Everything seems to work well, I can even select a number of questions. When writing this, I thought to myself 
'Hm, but what happens if the player chooses a number that is smaller than or larger than the minimum/maximum amount
of questions that are in the quiz?'. I'd apparently already thought about this before - the quiz won't initialize if
you do this. Instead, a message stating something along the lines of 'please format your request properly' is returned.

Hm. You can play the quiz through the API now. I'll kind of just ignore question creation, modification, removal for now.
That's a problem for a later time, I'll prioritize making it possible to play the quiz using the API for now. Both the
command-line based project and the desktop project use InterfaceHandler, so if I change InterfaceHandler to use the
API, both of those should work. Then it's just the React application left.

Alas, I have run into a big problem. I'm trying to use a HTTP GET request to initialize the quiz with the 
InitializeQuiz() method in QuizController. However, it is currently not working. The HTTP client seems to be disposed
(I'm not fully certain of the meaning, only vaguely), as I'm getting the following error message: 
```chsarp
Exception: Cannot access a disposed object.
Object name: 'System.Net.Http.HttpClient'.
System.Threading.Tasks.Task`1[System.String]
```
So that's not good. 

2024-02-06
-----------
I think I've fixed the above issue, I figured from the start that the problem was the 'using' statement. As I expected,
the HttpClient was being disposed after the first using statement, and since InterfaceHandler was trying to use the 
same HttpClient for each API request, it was trying to use a disposed client. So, the easy fix was to remove 'using' 
and implement proper HttpClient disposing afterwards.

I've run into another issue that I can't seem to solve. I'm attempting to return a string 'Quiz has been initialized...'
upon initializing the quiz. The initialization itself is successful - I can see the new column in SQL Server Management
Studio. But an error is being thrown when I'm trying to return the string that is returned from the API, like this:
`return await response.Content.ReadFromJsonAsync<string>();`. However, I'm getting this error message: 
`Exception: 'Q' is an invalid start of a value.`, and I don't know why.

Some time later, I've changed respective methods in QuizService, InitializeQuizCommand, and QuizController. Now, 
QuizController doesn't return the string, but a property containing the string (the property replaced a previous property
in InitializeQuizCommand). Still, text is returned rather than JSON. And now I *really* don't know why.

After far more pain and debugging that I would like to admit, I've got it working at last. I had to resort to strategies
I would consider a little low, but it works. I changed the data type of the property mentioned above from string to 
List<string>. It works, and successfully converts to a JSON object as I was hoping.
* https://stackoverflow.com/questions/36698677/why-is-this-httpclient-usage-giving-me-an-cannot-access-a-disposed-object-err
* https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/

