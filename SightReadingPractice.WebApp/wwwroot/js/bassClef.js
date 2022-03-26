
const clefType = 0;

const drawClef = (x, y, size) => {
    context.drawImage(document.getElementById("bass-clef"), x - 20, y - 15 + oneOuterSpace, size, size);
}