
const clefType = 1;

const drawClef = (x, y, size) => {
    context.drawImage(document.getElementById("treble-clef"), x - 10, y - 25 + oneOuterSpace, size - 30, size + 45);
}