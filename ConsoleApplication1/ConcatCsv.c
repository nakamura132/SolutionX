#include "ConcatCsv.h"

/// <summary>
/// 2つのCSV文字列を結合する
/// </summary>
/// <param name="csv1">1つ目のCSV文字列の開始アドレス</param>
/// <param name="csv2">2つ目のCSV文字列の開始アドレス</param>
/// <param name="csvOut">出力文字列バッファの開始アドレス</param>
/// <returns>正常に結合できた場合は 0、何らかのエラーで失敗した場合は -1 </returns>
int ConcatCsv(const char* csv1, const char* csv2, char* csvOut) {
	// この関数では入力として与えられた2つのCSV文字列を結合して１つのCSVにして返す
	// 入力1 : csv1
	// 入力2 : csv2
	// 出力  : csvOut
	// 戻り値 : 処理結果コード
	//
	// 制約
	// 入力として与えられるCSV文字列は以下の形式とする
	// ・ヘッダ１行とデータ１行の計２行
	// ・ヘッダ行とデータ行の列数は等しい
	// ・カンマで区切られたフィールドは "" で必ず囲まれる
	// 
	// 例えば以下の形式はNGである
	// × 3行ある
	// "CPU","Memory","Disk"
	// "Core i5","16GB","1TB"
	// "Core i7","8GB","2TB"
	//
	// × 列数が不一致
	// "CPU","Memory","Disk"
	// "Core i5","16GB","1TB","Windows 11"
	// 
	// × "" で囲まれていない箇所がある
	// "CPU","Memory","Disk"
	// "Core i5",16GB,"1TB"
	//
	// ・出力として返されるCSVもヘッダ１行とデータ１行の計２行である
	//
	// 処理の流れは以下の通り
	// ・入力のCSVを行単位に分解する
	// ・それぞれの行ごとに結合する( 入力1の1行目＋入力2の1行目, 入力1の2行目＋入力2の2行目 )
	// ・結合した行を１つの文字列に結合する( 結合した1行目 + \n + 結合した2行目 )
	//
	// 戻り値
	// ・正常に結合できた場合は 0、何らかのエラーで失敗した場合は -1 を返す
	//

	// ==================== ・入力のCSVを行単位に分解する ==================== 
	// 文字列走査のためのポインタをコピー元とコピー先の２つ用意する

	// コピー元文字列走査用ポインタ
	char* src_cursor;
	// コピー先文字列走査用ポインタ
	char* dst_cursor;

	// ==================== CSV1 の分割 ==================== 
	char header1[256];
	char data1[256];

	src_cursor = csv1;
	dst_cursor = header1;
	while (1) {
		if (*src_cursor == '\0') {
			// 改行が来る前に文字列の終端にきてしまった。制約違反のためエラー終了する
			return -1;
		}
		if (*src_cursor == '\n') {
			// 改行に到着。ここでヘッダ行は終わりなのでループを抜ける
			break;
		}
		// 入力のCSVの１文字をヘッダ格納用バッファにコピー
		*dst_cursor = *src_cursor;

		src_cursor++;
		dst_cursor++;
	};

	// ヘッダ格納用バッファの最後に終端文字を付与する
	*dst_cursor = '\0';
	// 改行は飛ばすので１つ進める
	src_cursor++;

	dst_cursor = data1;
	while (1) {
		if (*src_cursor == '\0') {
			// 文字列の終端に到着。ここでデータ行は終わりなのでループを抜ける
			break;
		}
		// 入力のCSVの１文字をヘッダ格納用バッファにコピー
		*dst_cursor = *src_cursor;

		src_cursor++;
		dst_cursor++;
	};

	// データ格納用バッファの最後に終端文字を付与する
	*dst_cursor = '\0';

	// ==================== CSV2 の分割 ==================== 
	char header2[256];
	char data2[256];

	src_cursor = csv2;
	dst_cursor = header2;
	while (1) {
		if (*src_cursor == '\0') {
			// 改行が来る前に文字列の終端にきてしまった。制約違反のためエラー終了する
			return -1;
		}
		if (*src_cursor == '\n') {
			// 改行に到着。ここでヘッダ行は終わりなのでループを抜ける
			break;
		}
		// 入力のCSVの１文字をヘッダ格納用バッファにコピー
		*dst_cursor = *src_cursor;

		src_cursor++;
		dst_cursor++;
	};

	// ヘッダ格納用バッファの最後に終端文字を付与する
	*dst_cursor = '\0';
	// 改行は飛ばすので１つ進める
	src_cursor++;

	dst_cursor = data2;
	while (1) {
		if (*src_cursor == '\0') {
			// 文字列の終端に到着。ここでデータ行は終わりなのでループを抜ける
			break;
		}
		// 入力のCSVの１文字をヘッダ格納用バッファにコピー
		*dst_cursor = *src_cursor;

		src_cursor++;
		dst_cursor++;
	};

	// データ格納用バッファの最後に終端文字を付与する
	*dst_cursor = '\0';

	// デバッグ用コード
	printf("header1 = %s\n", header1);
	printf("data1   = %s\n", data1);
	printf("header2 = %s\n", header2);
	printf("data2   = %s\n", data2);



	// ==================== ・それぞれの行ごとに結合する ====================
	// ==================== ・結合した行を１つの文字列に結合する ==================== 
	dst_cursor = csvOut;
	src_cursor = header1;
	while (1)
	{
		if (*src_cursor == '\0') {
			// ヘッダ格納バッファの終端に到着。ヘッダ1のコピー終わり
			break;
		}
		*dst_cursor = *src_cursor;

		src_cursor++;
		dst_cursor++;
	}

	// 区切り文字を付与する
	*dst_cursor = ',';
	dst_cursor++;

	src_cursor = header2;
	while (1)
	{
		if (*src_cursor == '\0') {
			// ヘッダ格納バッファの終端に到着。ヘッダ2のコピー終わり
			break;
		}
		*dst_cursor = *src_cursor;

		src_cursor++;
		dst_cursor++;
	}

	// 改行を付与する
	*dst_cursor = '\n';
	dst_cursor++;

	src_cursor = data1;
	while (1)
	{
		if (*src_cursor == '\0') {
			// データ格納バッファの終端に到着。データ1のコピー終わり
			break;
		}
		*dst_cursor = *src_cursor;

		src_cursor++;
		dst_cursor++;
	}

	// 区切り文字を付与する
	*dst_cursor = ',';
	dst_cursor++;

	src_cursor = data2;
	while (1)
	{
		if (*src_cursor == '\0') {
			// データ格納バッファの終端に到着。データ2のコピー終わり
			break;
		}
		*dst_cursor = *src_cursor;

		src_cursor++;
		dst_cursor++;
	}

	// 文字列終端コードを付与する
	*dst_cursor = '\0';

	return 0;
}