const canvas = document.getElementById("sheet-music");
const context = canvas.getContext("2d");

const totalWidth = canvas.offsetWidth;
const totalHeight = canvas.offsetHeight;
const rowHeight = totalHeight / 10;
const symbolStep = rowHeight / 2;
const oneOuterSpace = rowHeight * 3;

const noteYcoordinate = clefType === 0 ? 12 * symbolStep : 12 * symbolStep - symbolStep * 2; // tone: A, septimaArea: 0
const keySignatureYcoordinate = noteYcoordinate + symbolStep * 2;                            // -"-

let notes;
let keySignatures;

const getExercise = () => {
    fetch('/api/SightReadingPractice/sheetSymbols?clefType=' + clefType)
        .then(resp => resp.json())
        .then(data => {
            notes = data.notes;
            keySignatures = data.keySignatures;

            drawClef(11, 0, 111);
            drawStaff(rowHeight, oneOuterSpace);
            drawKeySignatures(keySignatures, symbolStep, keySignatureYcoordinate);
            drawNotes(notes, symbolStep, noteYcoordinate, clefType);  
        });
}

window.onload = () => {
    getExercise();
}

const translateNotes = (notes, keySignatures) => {
    const naiveNotes = [...notes];
    const keySignaturesTones = keySignatures.map(k => k.tone);
    let finalNotes = [];

    naiveNotes.forEach(nt => {
        if (keySignaturesTones.includes(nt.tone)) {
            const signature = keySignatures.find(ks => ks.tone === nt.tone).signature;
            finalNotes.push({ tone: nt.tone + signature, septimaArea: nt.septimaArea });
        } else {
            finalNotes.push(nt);
        }
    });

    const naiveTranslationsToProper = [
        { Naive: 'Cb', Proper: 'B' },
        { Naive: 'E#', Proper: 'F' },
        { Naive: 'Fb', Proper: 'E' },
        { Naive: 'B#', Proper: 'C' }
    ];

    naiveTranslationsToProper.forEach(ntp => {
        finalNotes.filter(fn => fn.tone === ntp.Naive).forEach(fn => fn.tone = ntp.Proper);
    });

    return finalNotes;
}

const postExerciseResult = (dateTime, userAnswers, actualTones) => {
    let exerciseResult = [];

    for (let i = 0; i < userAnswers.length; i++) {
        exerciseResult.push({
            Note: actualTones[i],
            Success: userAnswers[i].toLowerCase() === actualTones[i].tone.toLowerCase()
        });
    }

    const inputModel = { ClefType: clefType, DateTime: dateTime.toJSON(), ExerciseResult: exerciseResult };

    console.log(JSON.stringify(inputModel));

    fetch('api/SightReadingPractice/exerciseResult', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(inputModel)
    });
}

const submitAnswer = () => {
    const dateTime = new Date(Date.now());

    const answerElements = document.getElementsByClassName('answer-box');
    const submitButton = document.getElementById('submit');

    if (submitButton.value === 'Next') {
        location.reload();
        submitButton.value = 'Submit';

        for (let i = 0; i < answerElements.length; i++) {
            answerElements[i].disabled = false;
        }
        return;
    }

    let userAnswers = [];
    for (let i = 0; i < answerElements.length; i++) {
        userAnswers.push(answerElements[i].value);
    }
    const actualTones = translateNotes(notes, keySignatures);

    let idealCount = 0;

    postExerciseResult(dateTime, userAnswers, actualTones);

    for (let i = 0; i < userAnswers.length; i++) {
        if (userAnswers[i].toLowerCase() == actualTones[i].tone.toLowerCase()) {
            answerElements[i].classList.remove('incorrect');
            answerElements[i].classList.add('correct');
            answerElements[i].disabled = true;
            idealCount++;
        } else {
            answerElements[i].classList.remove('correct');
            answerElements[i].classList.add('incorrect');
        }
    }

    if (idealCount === actualTones.length) {
        submitButton.value = 'Next';
    }
}

const drawStaff = (rowHeight, outerSpace) => {
    const explicitHeightOnly = rowHeight * 4;

    context.lineWidth = 2;
    context.beginPath();
    context.rect(0, outerSpace, totalWidth, explicitHeightOnly);
    context.stroke();

    for (let i = 1; i < 4; i++) {
        const y = outerSpace + (explicitHeightOnly / 4) * i;

        context.lineWidth = 2;
        context.moveTo(0, y);
        context.lineTo(totalWidth, y);
        context.stroke();
    }
}

const septimaRange = ['A', 'B', 'C', 'D', 'E', 'F', 'G'];

const getYcoordinate = (toneAndSeptimaArea, symbolStep, firstY) => {
    let latestY = firstY + ((symbolStep * septimaRange.length) * toneAndSeptimaArea.septimaArea);

    for (let i = 0; i < septimaRange.length; i++) {
        if (septimaRange[i] == toneAndSeptimaArea.tone) {
            break;
        }
        latestY -= symbolStep;
    }
    return latestY;
}

const drawKeySignatures = (keySignatures, symbolStep, firstY) => {
    let x = 95;

    const drawSignature = (signature, x, y) => {
        if (signature == '#') {
            y += 3;
        }
        context.beginPath();
        context.fillStyle = "black";
        context.font = "italic 40px Arial";
        context.fillText(signature, x, y);
    }

    keySignatures.forEach(keySignature => {
        drawSignature(keySignature.signature, x, getYcoordinate({ tone: keySignature.tone, septimaArea: 0 },
            symbolStep,
            firstY));
        x += 30;
    });
}

const drawEllipse = (x, y, w, h) => {
    const kappa = .5522848,
        ox = (w / 2) * kappa, // control point offset horizontal
        oy = (h / 2) * kappa, // control point offset vertical
        xe = x + w,           // x-end
        ye = y + h,           // y-end
        xm = x + w / 2,       // x-middle
        ym = y + h / 2;       // y-middle

    context.beginPath();
    context.moveTo(x, ym);
    context.bezierCurveTo(x, ym - oy, xm - ox, y, xm, y);
    context.bezierCurveTo(xm + ox, y, xe, ym - oy, xe, ym);
    context.bezierCurveTo(xe, ym + oy, xm + ox, ye, xm, ye);
    context.bezierCurveTo(xm - ox, ye, x, ym + oy, x, ym);
    context.stroke();
}

const drawNotes = (noteList, symbolStep, firstY, clefType) => {
    const isOutsideStaff = (note, clefType) => {
        if (note.septimaArea === 0) {
            return false;
        }
        else if (clefType === 0) {
            return !(note.septimaArea == 1 && note.tone == 'G' || note.tone == 'F' || note.septimaArea == -1 && note.tone == 'A' || note.tone == 'B');
        }
        else {
            return note.septimaArea == -1 || note.septimaArea == 1 && (note.tone == 'C' || note.tone == 'B' || note.tone == 'A');
        }
    }

    const drawHelpLines = (notesX, septimaArea) => {
        const initialFactor = septimaArea === 1 ? 0 : 7;

        for (let i = initialFactor; i < initialFactor + 2; i++) {
            const y = totalHeight - rowHeight - i * rowHeight;

            context.beginPath();
            context.lineWidth = 2;
            context.moveTo(notesX - 11, y);
            context.lineTo(notesX + 39, y);
            context.stroke();
        }
    }

    const noteTailRightAndAbove = (note) => {
        return (note.septimaArea === 1 || (note.septimaArea === 0 && septimaRange.indexOf(note.tone) <= 2));
    }

    const drawNote = (notesX, notesY, noteTailRightAndAbove) => {
        context.beginPath();
        context.fillStyle = "black";
        drawEllipse(notesX, notesY, 28, 23);
        context.fill();

        const tailsX = notesX + 27;
        const tailsFromY = notesY + 15;
        const tailsToY = noteTailRightAndAbove ? notesY - 70 : notesY + 70;

        context.beginPath();
        context.lineWidth = 2;
        context.moveTo(tailsX, tailsFromY);
        context.lineTo(tailsX, tailsToY);
        context.stroke();
    }

    let x = totalWidth / 3.2;

    noteList.forEach(note => {
        const y = getYcoordinate(note, symbolStep, firstY);

        if (isOutsideStaff(note, clefType)) {
            drawHelpLines(x, note.septimaArea);
        }

        drawNote(x, y, noteTailRightAndAbove(note));
        x += 200;
    });
}
