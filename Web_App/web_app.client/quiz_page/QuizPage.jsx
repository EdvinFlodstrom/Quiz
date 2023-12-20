import React from "react";
import InitialInstructions from "../components/InitialInstructions";
import TakeQuiz from "../components/TakeQuiz";
import CreateQuestion from "../components/CreateQuestion";
import RemoveQuestion from "../components/RemoveQuestion";
import ModifyQuestion from "../components/ModifyQuestion";
import CloseApplication from "../components/CloseApplication";

const QuizPage = () => {
    return (
        <>
        <h1>Quiz</h1>
        <InitialInstructions></InitialInstructions>
        </>
      );
};

export default QuizPage;