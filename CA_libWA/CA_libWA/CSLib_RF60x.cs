using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace CA_libWA
{
    /// <summary>
    /// Класс-оболочка для библиотеки "RF60x.dll"
    /// </summary>
    class CSLib_RF60x
    {
        #region PARAMS
            /// <summary>
            /// Состояние питания датчика
            /// </summary>
            public static UInt16 RF60x_PARAMETER_POWER_STATE = ((0x00) | ((0x01) << 8));
            /// <summary>
            /// Подключение  аналогового выхода
            /// </summary>
            public static UInt16 RF60x_PARAMETER_ANALOG_OUT         = ((0x01) | ((0x01) << 8));
            /// <summary>
            /// Управление выборкой и синхронизацией
            /// </summary>
            public static UInt16 RF60x_PARAMETER_SAMPLE_AND_SYNC    = ((0x02) | ((0x01) << 8));
            /// <summary>
            /// Сетевой адрес
            /// </summary>
            public static UInt16 RF60x_PARAMETER_NETWORK_ADDRESS    = ((0x03) | ((0x01) << 8));
            /// <summary>
            /// Скорость передачи данных через  по-следовательный порт
            /// </summary>
            public static UInt16 RF60x_PARAMETER_BAUDRATE           = ((0x04) | ((0x01) << 8));
            /// <summary>
            /// Яркость лазера
            /// </summary>
            public static UInt16 RF60x_PARAMETER_LASER_BRIGHT       = ((0x05) | ((0x01) << 8));
            /// <summary>
            /// Количество усредняемых значений
            /// </summary>
            public static UInt16 RF60x_PARAMETER_AVERAGE_COUNT      = ((0x06) | ((0x01) << 8));
            /// <summary>
            /// Период выборки
            /// </summary>
            public static UInt16 RF60x_PARAMETER_SAMPLING_PERIOD    = ((0x08) | ((0x02) << 8));
            /// <summary>
            /// Максимальное время накопления
            /// </summary>
            public static UInt16 RF60x_PARAMETER_ACCUMULATION_TIME  = ((0x0A) | ((0x02) << 8));
            /// <summary>
            /// Начало диапазона аналогового выхода
            /// </summary>
            public static UInt16 RF60x_PARAMETER_BEGIN_ANALOG_RANGE = ((0x0C) | ((0x02) << 8));
            /// <summary>
            /// Конец диапазона аналогового выхода
            /// </summary>
            public static UInt16 RF60x_PARAMETER_END_ANALOG_RANGE   = ((0x0E) | ((0x02) << 8));
            /// <summary>
            /// Время задержки результата
            /// </summary>
            public static UInt16 RF60x_PARAMETER_RESULT_DELAY_TIME  = ((0x10) | ((0x01) << 8));
            /// <summary>
            /// Точка нуля
            /// </summary>
            public static UInt16 RF60x_PARAMETER_ZERO_POINT_VALUE   = ((0x17) | ((0x02) << 8));

            public static UInt16 RF60x_PARAMETER_CAN_SPEED          = ((0x20) | ((0x01) << 8));
            public static UInt16 RF60x_PARAMETER_CAN_STANDARD_ID    = ((0x22) | ((0x02) << 8));
            public static UInt16 RF60x_PARAMETER_CAN_EXTENDED_ID    = ((0x24) | ((0x04) << 8));
            public static UInt16 RF60x_PARAMETER_CAN_ID             = ((0x28) | ((0x01) << 8));
        #endregion


        #region STRUCTS
            /// <summary>
            /// Структура-идентификатор устройства
            /// </summary>
            public struct _RF60x_HELLO_ANSWER_
            {
                /// <summary>
                /// тип устройства(для RF60x=60)
                /// </summary>
                public byte bDeviceType;
                /// <summary>
                /// модификация программного обеспечения
                /// </summary>
                public byte bDeviceModification;
                /// <summary>
                /// серийный номер устройства
                /// </summary>
                public UInt16 wDeviceSerial;
                /// <summary>
                /// значение базового расстояния для датчика
                /// </summary>
                public UInt16 wDeviceMaxDistance;
                /// <summary>
                /// значение диапазона для датчика
                /// </summary>
                public UInt16 wDeviceRange;
            }; 
        #endregion


        #region FUNCTIONS_RS232

            /// <summary>
            /// Подключение к COM-порту 
            /// </summary>
            /// <param name="portName">Название СОМ-порта(например"COM1:)"</param>
            /// <param name="dwSpeed">Скорость работы через COM порт</param>
            /// <param name="lpHandle">Указатель на дескриптор устройства</param>
            /// <returns>TRUE если подключение успешно,FALSE если подключение не удалось</returns>
            [DllImport("RF60x.dll")]
            public extern static bool RF60x_OpenPort(string portName, UInt32 dwSpeed, ref IntPtr lpHandle);

            /// <summary>
            /// Отключение от COM-порта
            /// </summary>
            /// <param name="hHandle">дескриптор устройства, полученный в результате работы функции RF60x_OpenPort()</param>
            /// <returns>TRUE если удалось отключиться от порта,FALSE при возникновении ошибки</returns>
            [DllImport("RF60x.dll")]
            public extern static bool RF60x_ClosePort(IntPtr hHandle);

            /// <summary>
            /// Идентификация устройства и заполнение структуры _RF60x_HELLO_ANSWER_
            /// </summary>
            /// <param name="hCOM">дескриптор устройства, полученный в результате работы функции RF60x_OpenPort()</param>
            /// <param name="bAddress">адрес устройства</param>
            /// <param name="lprfHelloAnswer">указатель(ссылка) на структуру _RF60x_HELLO_ANSWER_</param>
            /// <returns>TRUE если устройство ответило на идентификацию, в противном случае FALSE</returns>
            [DllImport("RF60x.dll")]
            public extern static bool RF60x_HelloCmd(IntPtr hCOM, byte bAddress, ref _RF60x_HELLO_ANSWER_ lprfHelloAnswer);

            /// <summary>
            /// Чтение параметров
            /// </summary>
            /// <param name="hCOM">дескриптор устройства, полученный в результате работы функции RF60x_OpenPort()</param>
            /// <param name="bAddress">адрес устройства</param>
            /// <param name="wParameter">номер параметра</param>
            /// <param name="lpdwValue">переменная для хранения текущего значения параметров</param>
            /// <returns>TRUE если устройство ответило на запрос параметра и заполняет lpdwValue, иначе возвращает FALSE</returns>
            [DllImport("RF60x.dll")]
            public extern static bool RF60x_ReadParameter(IntPtr hCOM, byte bAddress, UInt16 wParameter, ref UInt32 lpdwValue);

            /// <summary>
            /// Запись параметров
            /// </summary>
            /// <param name="hCOM">дескриптор устройства, полученный в результате работы функции RF60x_OpenPort()</param>
            /// <param name="bAddress">адрес устройства</param>
            /// <param name="wParameter">номер параметра</param>
            /// <param name="dwValue">переменная для вывода текущего значения параметров</param>
            /// <returns>TRUE если устройство ответило на запрос записи параметра, иначе FALSE</returns>
            [DllImport("RF60x.dll")]
            public extern static bool RF60x_WriteParameter(IntPtr hCOM, byte bAddress, UInt16 wParameter, UInt32 dwValue);

            /// <summary>
            /// Сохранение текущих параметров во FLASH-памяти датчика
            /// </summary>
            /// <param name="hCOM">дескриптор устройства, полученный в результате работы функции RF60x_OpenPort()</param>
            /// <param name="bAddress">адрес устройства</param>
            /// <returns>TRUE если устройство ответило на запрос сохранения параметров, иначе FALSE</returns>
            [DllImport("RF60x.dll")]
            public extern static bool RF60x_FlushToFlash(IntPtr hCOM, byte bAddress);

            /// <summary>
            /// Восстановление параметров из FLASH-памяти
            /// </summary>
            /// <param name="hCOM">дескриптор устройства, полученный в результате работы функции RF60x_OpenPort()</param>
            /// <param name="bAddress">адрес устройства</param>
            /// <returns>TRUE если устройство ответило на запрос восстановления параметров, иначе FALSE</returns>
            [DllImport("RF60x.dll")]
            public extern static bool RF60x_RestoreFromFlash(IntPtr hCOM, byte bAddress);

            /// <summary>
            /// Защелкивание текущего результата
            /// </summary>
            /// <param name="hCOM">дескриптор устройства, полученный в результате работы функции RF60x_OpenPort()</param>
            /// <param name="bAddress">адрес устройства</param>
            /// <returns>TRUE если устройство ответило на запрос защелкивания результата, иначе FALSE</returns>
            [DllImport("RF60x.dll")]
            public extern static bool RF60x_LockResult(IntPtr hCOM, byte bAddress);

            /// <summary>
            /// Чтение данных из датчика текущего измеренного значения. Значение передаваемого датчиком результата (D) нормировано таким образом, чтобы полному  диапазону  датчика (S  в мм)  соответствовала  величина 4000h (16384), поэтому результат в миллиметрах получают по следующей формуле: X=D*S/4000h (мм)
            /// </summary>
            /// <param name="hCOM">дескриптор устройства, полученный в результате работы функции RF60x_OpenPort()</param>
            /// <param name="bAddress">адрес устройства</param>
            /// <param name="lpusValue">указатель(ссылка) на переменную  содержащую результат D</param>
            /// <returns>TRUE если устройство ответило на запрос, иначе FALSE</returns>
            [DllImport("RF60x.dll")]
            public extern static bool RF60x_Measure(IntPtr hCOM, byte bAddress, ref UInt16 lpusValue);

            /// <summary>
            /// Получает значение в миллиметрах исходя из результатов функции RF60x_Measure()
            /// </summary>
            /// <param name="Darg">параметр D, результат датчика</param>
            /// <param name="Sarg">параметр S, полный диапазон датчика</param>
            /// <returns>возвращает значение расстояния в миллиметрах</returns>
            public static float DToXTransform(UInt16 Darg, UInt16 Sarg)
            {
                return (float)(Darg * Sarg) / 0x4000;
            }

            /// <summary>
            /// Запуск потока измерений
            /// </summary>
            /// <param name="hCOM">дескриптор устройства, полученный в результате работы функции RF60x_OpenPort()</param>
            /// <param name="bAddress">адрес устройства</param>
            /// <returns>TRUE если удалось перевести устройство в режим непрерывной передачи результатов, инача FALSE</returns>
            [DllImport("RF60x.dll")]
            public extern static bool RF60x_StartStream(IntPtr hCOM, byte bAddress);

            /// <summary>
            /// Остановка потока измерений
            /// </summary>
            /// <param name="hCOM">дескриптор устройства, полученный в результате работы функции RF60x_OpenPort()</param>
            /// <param name="bAddress">адрес устройства</param>
            /// <returns>TRUE если удалось остановить передачу данных, иначе FALSE</returns>
            [DllImport("RF60x.dll")]
            public extern static bool RF60x_StopStream(IntPtr hCOM, byte bAddress);

            /// <summary>
            /// Получение результатов измерения из потока
            /// </summary>
            /// <param name="hCOM">дескриптор устройства, полученный в результате работы функции RF60x_OpenPort()</param>
            /// <param name="lpusValue">указатель на переменную, содержащую результат D(см. DToXTransform()) </param>
            /// <returns>TRUE если данные в буфере присутствуют и заполняется значение lpusValue, иначе FALSE</returns>
            [DllImport("RF60x.dll")]
            public extern static bool RF60x_GetStreamMeasure(IntPtr hCOM, ref UInt16 lpusValue);

            /// <summary>
            /// Передача пользовательских команд
            /// </summary>
            /// <param name="hCOM">дескриптор устройства, полученный в результате работы функции RF60x_OpenPort()</param>
            /// <param name="pcInData">массив для передачи в датчик</param>
            /// <param name="dwInSize">размер передаваемых данных </param>
            /// <param name="pcOutData">массив для приема данных из датчика</param>
            /// <param name="pdwOutSize">указатель на переменную, в которую будет записан размер полученных данных от датчика</param>
            /// <returns></returns>
            [DllImport("RF60x.dll")]
            public extern static bool RF60x_CustomCmd(IntPtr hCOM, String pcInData, UInt32 dwInSize, String pcOutData, ref UInt32 pdwOutSize);
        
       #endregion
    }

}
