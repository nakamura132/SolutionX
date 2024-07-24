#include "ConcatCsv.h"

/// <summary>
/// 2��CSV���������������
/// </summary>
/// <param name="csv1">1�ڂ�CSV������̊J�n�A�h���X</param>
/// <param name="csv2">2�ڂ�CSV������̊J�n�A�h���X</param>
/// <param name="csvOut">�o�͕�����o�b�t�@�̊J�n�A�h���X</param>
/// <returns>����Ɍ����ł����ꍇ�� 0�A���炩�̃G���[�Ŏ��s�����ꍇ�� -1 </returns>
int ConcatCsv(const char* csv1, const char* csv2, char* csvOut) {
	// ���̊֐��ł͓��͂Ƃ��ė^����ꂽ2��CSV��������������ĂP��CSV�ɂ��ĕԂ�
	// ����1 : csv1
	// ����2 : csv2
	// �o��  : csvOut
	// �߂�l : �������ʃR�[�h
	//
	// ����
	// ���͂Ƃ��ė^������CSV������͈ȉ��̌`���Ƃ���
	// �E�w�b�_�P�s�ƃf�[�^�P�s�̌v�Q�s
	// �E�w�b�_�s�ƃf�[�^�s�̗񐔂͓�����
	// �E�J���}�ŋ�؂�ꂽ�t�B�[���h�� "" �ŕK���͂܂��
	// 
	// �Ⴆ�Έȉ��̌`����NG�ł���
	// �~ 3�s����
	// "CPU","Memory","Disk"
	// "Core i5","16GB","1TB"
	// "Core i7","8GB","2TB"
	//
	// �~ �񐔂��s��v
	// "CPU","Memory","Disk"
	// "Core i5","16GB","1TB","Windows 11"
	// 
	// �~ "" �ň͂܂�Ă��Ȃ��ӏ�������
	// "CPU","Memory","Disk"
	// "Core i5",16GB,"1TB"
	//
	// �E�o�͂Ƃ��ĕԂ����CSV���w�b�_�P�s�ƃf�[�^�P�s�̌v�Q�s�ł���
	//
	// �����̗���͈ȉ��̒ʂ�
	// �E���͂�CSV���s�P�ʂɕ�������
	// �E���ꂼ��̍s���ƂɌ�������( ����1��1�s�ځ{����2��1�s��, ����1��2�s�ځ{����2��2�s�� )
	// �E���������s���P�̕�����Ɍ�������( ��������1�s�� + \n + ��������2�s�� )
	//
	// �߂�l
	// �E����Ɍ����ł����ꍇ�� 0�A���炩�̃G���[�Ŏ��s�����ꍇ�� -1 ��Ԃ�
	//

	// ==================== �E���͂�CSV���s�P�ʂɕ������� ==================== 
	// �����񑖍��̂��߂̃|�C���^���R�s�[���ƃR�s�[��̂Q�p�ӂ���

	// �R�s�[�������񑖍��p�|�C���^
	char* src_cursor;
	// �R�s�[�敶���񑖍��p�|�C���^
	char* dst_cursor;

	// ==================== CSV1 �̕��� ==================== 
	char header1[256];
	char data1[256];

	src_cursor = csv1;
	dst_cursor = header1;
	while (1) {
		if (*src_cursor == '\0') {
			// ���s������O�ɕ�����̏I�[�ɂ��Ă��܂����B����ᔽ�̂��߃G���[�I������
			return -1;
		}
		if (*src_cursor == '\n') {
			// ���s�ɓ����B�����Ńw�b�_�s�͏I���Ȃ̂Ń��[�v�𔲂���
			break;
		}
		// ���͂�CSV�̂P�������w�b�_�i�[�p�o�b�t�@�ɃR�s�[
		*dst_cursor = *src_cursor;

		src_cursor++;
		dst_cursor++;
	};

	// �w�b�_�i�[�p�o�b�t�@�̍Ō�ɏI�[������t�^����
	*dst_cursor = '\0';
	// ���s�͔�΂��̂łP�i�߂�
	src_cursor++;

	dst_cursor = data1;
	while (1) {
		if (*src_cursor == '\0') {
			// ������̏I�[�ɓ����B�����Ńf�[�^�s�͏I���Ȃ̂Ń��[�v�𔲂���
			break;
		}
		// ���͂�CSV�̂P�������w�b�_�i�[�p�o�b�t�@�ɃR�s�[
		*dst_cursor = *src_cursor;

		src_cursor++;
		dst_cursor++;
	};

	// �f�[�^�i�[�p�o�b�t�@�̍Ō�ɏI�[������t�^����
	*dst_cursor = '\0';

	// ==================== CSV2 �̕��� ==================== 
	char header2[256];
	char data2[256];

	src_cursor = csv2;
	dst_cursor = header2;
	while (1) {
		if (*src_cursor == '\0') {
			// ���s������O�ɕ�����̏I�[�ɂ��Ă��܂����B����ᔽ�̂��߃G���[�I������
			return -1;
		}
		if (*src_cursor == '\n') {
			// ���s�ɓ����B�����Ńw�b�_�s�͏I���Ȃ̂Ń��[�v�𔲂���
			break;
		}
		// ���͂�CSV�̂P�������w�b�_�i�[�p�o�b�t�@�ɃR�s�[
		*dst_cursor = *src_cursor;

		src_cursor++;
		dst_cursor++;
	};

	// �w�b�_�i�[�p�o�b�t�@�̍Ō�ɏI�[������t�^����
	*dst_cursor = '\0';
	// ���s�͔�΂��̂łP�i�߂�
	src_cursor++;

	dst_cursor = data2;
	while (1) {
		if (*src_cursor == '\0') {
			// ������̏I�[�ɓ����B�����Ńf�[�^�s�͏I���Ȃ̂Ń��[�v�𔲂���
			break;
		}
		// ���͂�CSV�̂P�������w�b�_�i�[�p�o�b�t�@�ɃR�s�[
		*dst_cursor = *src_cursor;

		src_cursor++;
		dst_cursor++;
	};

	// �f�[�^�i�[�p�o�b�t�@�̍Ō�ɏI�[������t�^����
	*dst_cursor = '\0';

	// �f�o�b�O�p�R�[�h
	printf("header1 = %s\n", header1);
	printf("data1   = %s\n", data1);
	printf("header2 = %s\n", header2);
	printf("data2   = %s\n", data2);



	// ==================== �E���ꂼ��̍s���ƂɌ������� ====================
	// ==================== �E���������s���P�̕�����Ɍ������� ==================== 
	dst_cursor = csvOut;
	src_cursor = header1;
	while (1)
	{
		if (*src_cursor == '\0') {
			// �w�b�_�i�[�o�b�t�@�̏I�[�ɓ����B�w�b�_1�̃R�s�[�I���
			break;
		}
		*dst_cursor = *src_cursor;

		src_cursor++;
		dst_cursor++;
	}

	// ��؂蕶����t�^����
	*dst_cursor = ',';
	dst_cursor++;

	src_cursor = header2;
	while (1)
	{
		if (*src_cursor == '\0') {
			// �w�b�_�i�[�o�b�t�@�̏I�[�ɓ����B�w�b�_2�̃R�s�[�I���
			break;
		}
		*dst_cursor = *src_cursor;

		src_cursor++;
		dst_cursor++;
	}

	// ���s��t�^����
	*dst_cursor = '\n';
	dst_cursor++;

	src_cursor = data1;
	while (1)
	{
		if (*src_cursor == '\0') {
			// �f�[�^�i�[�o�b�t�@�̏I�[�ɓ����B�f�[�^1�̃R�s�[�I���
			break;
		}
		*dst_cursor = *src_cursor;

		src_cursor++;
		dst_cursor++;
	}

	// ��؂蕶����t�^����
	*dst_cursor = ',';
	dst_cursor++;

	src_cursor = data2;
	while (1)
	{
		if (*src_cursor == '\0') {
			// �f�[�^�i�[�o�b�t�@�̏I�[�ɓ����B�f�[�^2�̃R�s�[�I���
			break;
		}
		*dst_cursor = *src_cursor;

		src_cursor++;
		dst_cursor++;
	}

	// ������I�[�R�[�h��t�^����
	*dst_cursor = '\0';

	return 0;
}