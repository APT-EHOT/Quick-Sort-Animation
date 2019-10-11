#include <iostream>
#include <ctime>

using namespace std;

const int N = 20;
int numbers[N];

void shuffleArray(int * arrayToShuffle, int left, int right) {

    srand(time(0));
    int randomSwap = 100 + rand() % 1000;


    for (int i = 0; i < randomSwap; i++) {
        int randomPoint = rand() % 19;
        swap(arrayToShuffle[randomPoint], arrayToShuffle[randomPoint+1]);
    }

}

void quickSort(int * arrayToSort, int leftBorder, int rightBorder) {

    int leftPointer = leftBorder;
    int rightPointer = rightBorder;
    int pivotElement = arrayToSort[ (leftPointer + rightPointer) / 2];

    do {

        while (arrayToSort[leftPointer] < pivotElement)
            leftPointer++;

        while (arrayToSort[rightPointer] > pivotElement)
            rightPointer--;

        if (leftPointer <= rightPointer) {
            swap(arrayToSort[leftPointer],
                    arrayToSort[rightPointer]);

            leftPointer++;
            rightPointer--;
        }

    } while (leftPointer < rightPointer);

    if (rightPointer > leftBorder)
        quickSort(arrayToSort, leftBorder, rightPointer);

    if (rightBorder > leftPointer)
        quickSort(arrayToSort, leftPointer, rightBorder);

}

int main() {


    for (int i = 0; i < N; i++)
        numbers[i] = i+1;

    shuffleArray(numbers, 0, N-1);

    for (int number : numbers)
        cout << number << " ";

    quickSort(numbers, 0, N-1);

    cout << "\n";

    for (int number : numbers)
        cout << number << " ";

    return 0;
}