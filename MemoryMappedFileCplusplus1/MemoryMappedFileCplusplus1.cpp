// MemoryMappedFileCplusplus1.cpp : このファイルには 'main' 関数が含まれています。プログラム実行の開始と終了がそこで行われます。
//

#include <iostream>
#include <Windows.h>
#include <string>

int main()
{
    // メモリマップトファイルを開く
    HANDLE hMapFile = OpenFileMapping(FILE_MAP_READ, FALSE, L"Local\\testmap");
    if (hMapFile == NULL)
    {
        std::cerr << "Could not open file mapping object (" << GetLastError() << ")." << std::endl;
        return 1;
    }

    // マップされたビューを取得
    LPVOID pBuf = MapViewOfFile(hMapFile, FILE_MAP_READ, 0, 0, 0);
    if (pBuf == NULL)
    {
        std::cerr << "Could not map view of file (" << GetLastError() << ")." << std::endl;
        CloseHandle(hMapFile);
        return 1;
    }

    // データの長さを最初の位置から読み込む
    int length = *(int*)pBuf;

    // メッセージを読み込む
    char* message = new char[length + 1];
    memcpy(message, (char*)pBuf + 4, length);
    message[length] = '\0';

    std::cout << "Message read from memory-mapped file: " << message << std::endl;

    // クリーンアップ
    delete[] message;
    UnmapViewOfFile(pBuf);
    CloseHandle(hMapFile);

    return 0;
}

// プログラムの実行: Ctrl + F5 または [デバッグ] > [デバッグなしで開始] メニュー
// プログラムのデバッグ: F5 または [デバッグ] > [デバッグの開始] メニュー

// 作業を開始するためのヒント: 
//    1. ソリューション エクスプローラー ウィンドウを使用してファイルを追加/管理します 
//   2. チーム エクスプローラー ウィンドウを使用してソース管理に接続します
//   3. 出力ウィンドウを使用して、ビルド出力とその他のメッセージを表示します
//   4. エラー一覧ウィンドウを使用してエラーを表示します
//   5. [プロジェクト] > [新しい項目の追加] と移動して新しいコード ファイルを作成するか、[プロジェクト] > [既存の項目の追加] と移動して既存のコード ファイルをプロジェクトに追加します
//   6. 後ほどこのプロジェクトを再び開く場合、[ファイル] > [開く] > [プロジェクト] と移動して .sln ファイルを選択します
