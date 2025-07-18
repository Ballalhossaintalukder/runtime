// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace System.Runtime.Intrinsics.X86
{
    /// <summary>Provides access to X86 AVX512DQ hardware instructions via intrinsics.</summary>
    [CLSCompliant(false)]
    public abstract class Avx512DQ : Avx512F
    {
        internal Avx512DQ() { }

        /// <summary>Gets a value that indicates whether the APIs in this class are supported.</summary>
        /// <value><see langword="true" /> if the APIs are supported; otherwise, <see langword="false" />.</value>
        /// <remarks>A value of <see langword="false" /> indicates that the APIs will throw <see cref="PlatformNotSupportedException" />.</remarks>
        public static new bool IsSupported { [Intrinsic] get { return false; } }

        /// <summary>Provides access to the x86 AVX512DQ+VL hardware instructions via intrinsics.</summary>
        public new abstract class VL : Avx512F.VL
        {
            internal VL() { }

            /// <summary>Gets a value that indicates whether the APIs in this class are supported.</summary>
            /// <value><see langword="true" /> if the APIs are supported; otherwise, <see langword="false" />.</value>
            /// <remarks>A value of <see langword="false" /> indicates that the APIs will throw <see cref="PlatformNotSupportedException" />.</remarks>
            public static new bool IsSupported { [Intrinsic] get { return false; } }

            /// <summary>
            ///   <para>__m128i _mm_broadcast_i32x2 (__m128i a)</para>
            ///   <para>  VBROADCASTI32x2 xmm1 {k1}{z}, xmm2/m64</para>
            /// </summary>
            public static Vector128<int> BroadcastPairScalarToVector128(Vector128<int> value) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m128i _mm_broadcast_i32x2 (__m128i a)</para>
            ///   <para>  VBROADCASTI32x2 xmm1 {k1}{z}, xmm2/m64</para>
            /// </summary>
            public static Vector128<uint> BroadcastPairScalarToVector128(Vector128<uint> value) { throw new PlatformNotSupportedException(); }

            /// <summary>
            ///   <para>__m256i _mm256_broadcast_i32x2 (__m128i a)</para>
            ///   <para>  VBROADCASTI32x2 ymm1 {k1}{z}, xmm2/m64</para>
            /// </summary>
            public static Vector256<int> BroadcastPairScalarToVector256(Vector128<int> value) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m256i _mm256_broadcast_i32x2 (__m128i a)</para>
            ///   <para>  VBROADCASTI32x2 ymm1 {k1}{z}, xmm2/m64</para>
            /// </summary>
            public static Vector256<uint> BroadcastPairScalarToVector256(Vector128<uint> value) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m256 _mm256_broadcast_f32x2 (__m128 a)</para>
            ///   <para>  VBROADCASTF32x2 ymm1 {k1}{z}, xmm2/m64</para>
            /// </summary>
            public static Vector256<float> BroadcastPairScalarToVector256(Vector128<float> value) { throw new PlatformNotSupportedException(); }

            /// <summary>
            ///   <para>__mmask8 _mm_fpclass_pd_mask (__m128d a, int c)</para>
            ///   <para>  VFPCLASSPD k2 {k1}, xmm2/m128/m64bcst, imm8</para>
            /// </summary>
            public static Vector128<double> Classify(Vector128<double> value, [ConstantExpected] byte control) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__mmask8 _mm_fpclass_ps_mask (__m128 a, int c)</para>
            ///   <para>  VFPCLASSPS k2 {k1}, xmm2/m128/m32bcst, imm8</para>
            /// </summary>
            public static Vector128<float> Classify(Vector128<float> value, [ConstantExpected] byte control) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__mmask8 _mm256_fpclass_pd_mask (__m256d a, int c)</para>
            ///   <para>  VFPCLASSPD k2 {k1}, ymm2/m256/m64bcst, imm8</para>
            /// </summary>
            public static Vector256<double> Classify(Vector256<double> value, [ConstantExpected] byte control) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__mmask8 _mm256_fpclass_ps_mask (__m256 a, int c)</para>
            ///   <para>  VFPCLASSPS k2 {k1}, ymm2/m256/m32bcst, imm8</para>
            /// </summary>
            public static Vector256<float> Classify(Vector256<float> value, [ConstantExpected] byte control) { throw new PlatformNotSupportedException(); }

            /// <summary>
            ///   <para>__m128d _mm_cvtepi64_pd (__m128i a)</para>
            ///   <para>  VCVTQQ2PD xmm1 {k1}{z}, xmm2/m128/m64bcst</para>
            /// </summary>
            public static Vector128<double> ConvertToVector128Double(Vector128<long> value) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m128d _mm_cvtepu64_pd (__m128i a)</para>
            ///   <para>  VCVTUQQ2PD xmm1 {k1}{z}, xmm2/m128/m64bcst</para>
            /// </summary>
            public static Vector128<double> ConvertToVector128Double(Vector128<ulong> value) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m128i _mm_cvtps_epi64 (__m128 a)</para>
            ///   <para>  VCVTPS2QQ xmm1 {k1}{z}, xmm2/m64/m32bcst</para>
            /// </summary>
            public static Vector128<long> ConvertToVector128Int64(Vector128<float> value) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m128i _mm_cvtpd_epi64 (__m128d a)</para>
            ///   <para>  VCVTPD2QQ xmm1 {k1}{z}, xmm2/m128/m64bcst</para>
            /// </summary>
            public static Vector128<long> ConvertToVector128Int64(Vector128<double> value) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m128i _mm_cvttps_epi64 (__m128 a)</para>
            ///   <para>  VCVTTPS2QQ xmm1 {k1}{z}, xmm2/m64/m32bcst</para>
            /// </summary>
            public static Vector128<long> ConvertToVector128Int64WithTruncation(Vector128<float> value) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m128i _mm_cvttpd_epi64 (__m128d a)</para>
            ///   <para>  VCVTTPD2QQ xmm1 {k1}{z}, xmm2/m128/m64bcst</para>
            /// </summary>
            public static Vector128<long> ConvertToVector128Int64WithTruncation(Vector128<double> value) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m128 _mm_cvtepi64_ps (__m128i a)</para>
            ///   <para>  VCVTQQ2PS xmm1 {k1}{z}, xmm2/m128/m64bcst</para>
            /// </summary>
            public static Vector128<float> ConvertToVector128Single(Vector128<long> value) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m128 _mm256_cvtepi64_ps (__m256i a)</para>
            ///   <para>  VCVTQQ2PS xmm1 {k1}{z}, ymm2/m256/m64bcst</para>
            /// </summary>
            public static Vector128<float> ConvertToVector128Single(Vector256<long> value) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m128 _mm_cvtepu64_ps (__m128i a)</para>
            ///   <para>  VCVTUQQ2PS xmm1 {k1}{z}, xmm2/m128/m64bcst</para>
            /// </summary>
            public static Vector128<float> ConvertToVector128Single(Vector128<ulong> value) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m128 _mm256_cvtepu64_ps (__m256i a)</para>
            ///   <para>  VCVTUQQ2PS xmm1 {k1}{z}, ymm2/m256/m64bcst</para>
            /// </summary>
            public static Vector128<float> ConvertToVector128Single(Vector256<ulong> value) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m128i _mm_cvtps_epu64 (__m128 a)</para>
            ///   <para>  VCVTPS2UQQ xmm1 {k1}{z}, xmm2/m64/m32bcst</para>
            /// </summary>
            public static Vector128<ulong> ConvertToVector128UInt64(Vector128<float> value) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m128i _mm_cvtpd_epu64 (__m128d a)</para>
            ///   <para>  VCVTPD2UQQ xmm1 {k1}{z}, xmm2/m128/m64bcst</para>
            /// </summary>
            public static Vector128<ulong> ConvertToVector128UInt64(Vector128<double> value) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m128i _mm_cvttps_epu64 (__m128 a)</para>
            ///   <para>  VCVTTPS2UQQ xmm1 {k1}{z}, xmm2/m64/m32bcst</para>
            /// </summary>
            public static Vector128<ulong> ConvertToVector128UInt64WithTruncation(Vector128<float> value) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m128i _mm_cvttpd_epu64 (__m128d a)</para>
            ///   <para>  VCVTTPD2UQQ xmm1 {k1}{z}, xmm2/m128/m64bcst</para>
            /// </summary>
            public static Vector128<ulong> ConvertToVector128UInt64WithTruncation(Vector128<double> value) { throw new PlatformNotSupportedException(); }

            /// <summary>
            ///   <para>__m256d _mm256_cvtepi64_pd (__m256i a)</para>
            ///   <para>  VCVTQQ2PD ymm1 {k1}{z}, ymm2/m256/m64bcst</para>
            /// </summary>
            public static Vector256<double> ConvertToVector256Double(Vector256<long> value) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m256d _mm256_cvtepu64_pd (__m256i a)</para>
            ///   <para>  VCVTUQQ2PD ymm1 {k1}{z}, ymm2/m256/m64bcst</para>
            /// </summary>
            public static Vector256<double> ConvertToVector256Double(Vector256<ulong> value) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m256i _mm256_cvtps_epi64 (__m128 a)</para>
            ///   <para>  VCVTPS2QQ ymm1 {k1}{z}, xmm2/m128/m32bcst</para>
            /// </summary>
            public static Vector256<long> ConvertToVector256Int64(Vector128<float> value) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m256i _mm256_cvtpd_epi64 (__m256d a)</para>
            ///   <para>  VCVTPD2QQ ymm1 {k1}{z}, ymm2/m256/m64bcst</para>
            /// </summary>
            public static Vector256<long> ConvertToVector256Int64(Vector256<double> value) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m256i _mm256_cvttps_epi64 (__m128 a)</para>
            ///   <para>  VCVTTPS2QQ ymm1 {k1}{z}, xmm2/m128/m32bcst</para>
            /// </summary>
            public static Vector256<long> ConvertToVector256Int64WithTruncation(Vector128<float> value) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m256i _mm256_cvttpd_epi64 (__m256d a)</para>
            ///   <para>  VCVTTPD2QQ ymm1 {k1}{z}, ymm2/m256/m64bcst</para>
            /// </summary>
            public static Vector256<long> ConvertToVector256Int64WithTruncation(Vector256<double> value) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m256i _mm256_cvtps_epu64 (__m128 a)</para>
            ///   <para>  VCVTPS2UQQ ymm1 {k1}{z}, xmm2/m128/m32bcst</para>
            /// </summary>
            public static Vector256<ulong> ConvertToVector256UInt64(Vector128<float> value) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m256i _mm256_cvtpd_epu64 (__m256d a)</para>
            ///   <para>  VCVTPD2UQQ ymm1 {k1}{z}, ymm2/m256/m64bcst</para>
            /// </summary>
            public static Vector256<ulong> ConvertToVector256UInt64(Vector256<double> value) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m256i _mm256_cvttps_epu64 (__m128 a)</para>
            ///   <para>  VCVTTPS2UQQ ymm1 {k1}{z}, xmm2/m128/m32bcst</para>
            /// </summary>
            public static Vector256<ulong> ConvertToVector256UInt64WithTruncation(Vector128<float> value) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m256i _mm256_cvttpd_epu64 (__m256d a)</para>
            ///   <para>  VCVTTPD2UQQ ymm1 {k1}{z}, ymm2/m256/m64bcst</para>
            /// </summary>
            public static Vector256<ulong> ConvertToVector256UInt64WithTruncation(Vector256<double> value) { throw new PlatformNotSupportedException(); }

            /// <summary>
            ///   <para>__m128i _mm_mullo_epi64 (__m128i a, __m128i b)</para>
            ///   <para>  VPMULLQ xmm1 {k1}{z}, xmm2, xmm3/m128/m64bcst</para>
            /// </summary>
            public static Vector128<long> MultiplyLow(Vector128<long> left, Vector128<long> right) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m128i _mm_mullo_epi64 (__m128i a, __m128i b)</para>
            ///   <para>  VPMULLQ xmm1 {k1}{z}, xmm2, xmm3/m128/m64bcst</para>
            /// </summary>
            public static Vector128<ulong> MultiplyLow(Vector128<ulong> left, Vector128<ulong> right) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m256i _mm256_mullo_epi64 (__m256i a, __m256i b)</para>
            ///   <para>  VPMULLQ ymm1 {k1}{z}, ymm2, ymm3/m256/m64bcst</para>
            /// </summary>
            public static Vector256<long> MultiplyLow(Vector256<long> left, Vector256<long> right) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m256i _mm256_mullo_epi64 (__m256i a, __m256i b)</para>
            ///   <para>  VPMULLQ ymm1 {k1}{z}, ymm2, ymm3/m256/m64bcst</para>
            /// </summary>
            public static Vector256<ulong> MultiplyLow(Vector256<ulong> left, Vector256<ulong> right) { throw new PlatformNotSupportedException(); }

            /// <summary>
            ///   <para>__m128 _mm_range_ps(__m128 a, __m128 b, int imm);</para>
            ///   <para>  VRANGEPS xmm1 {k1}{z}, xmm2, xmm3/m128/m32bcst, imm8</para>
            /// </summary>
            public static Vector128<float> Range(Vector128<float> left, Vector128<float> right, [ConstantExpected(Max = (byte)(0x0F))] byte control) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m128d _mm_range_pd(__m128d a, __m128d b, int imm);</para>
            ///   <para>  VRANGEPD xmm1 {k1}{z}, xmm2, xmm3/m128/m64bcst, imm8</para>
            /// </summary>
            public static Vector128<double> Range(Vector128<double> left, Vector128<double> right, [ConstantExpected(Max = (byte)(0x0F))] byte control) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m256 _mm256_range_ps(__m256 a, __m256 b, int imm);</para>
            ///   <para>  VRANGEPS ymm1 {k1}{z}, ymm2, ymm3/m256/m32bcst, imm8</para>
            /// </summary>
            public static Vector256<float> Range(Vector256<float> left, Vector256<float> right, [ConstantExpected(Max = (byte)(0x0F))] byte control) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m256d _mm256_range_pd(__m256d a, __m256d b, int imm);</para>
            ///   <para>  VRANGEPD ymm1 {k1}{z}, ymm2, ymm3/m256/m64bcst, imm8</para>
            /// </summary>
            public static Vector256<double> Range(Vector256<double> left, Vector256<double> right, [ConstantExpected(Max = (byte)(0x0F))] byte control) { throw new PlatformNotSupportedException(); }

            /// <summary>
            ///   <para>__m128 _mm_reduce_ps(__m128 a, int imm);</para>
            ///   <para>  VREDUCEPS xmm1 {k1}{z}, xmm2/m128/m32bcst, imm8</para>
            /// </summary>
            public static Vector128<float> Reduce(Vector128<float> value, [ConstantExpected] byte control) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m128d _mm_reduce_pd(__m128d a, int imm);</para>
            ///   <para>  VREDUCEPD xmm1 {k1}{z}, xmm2/m128/m64bcst, imm8</para>
            /// </summary>
            public static Vector128<double> Reduce(Vector128<double> value, [ConstantExpected] byte control) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m256 _mm256_reduce_ps(__m256 a, int imm);</para>
            ///   <para>  VREDUCEPS ymm1 {k1}{z}, ymm2/m256/m32bcst, imm8</para>
            /// </summary>
            public static Vector256<float> Reduce(Vector256<float> value, [ConstantExpected] byte control) { throw new PlatformNotSupportedException(); }
            /// <summary>
            ///   <para>__m256d _mm256_reduce_pd(__m256d a, int imm);</para>
            ///   <para>  VREDUCEPD ymm1 {k1}{z}, ymm2/m256/m64bcst, imm8</para>
            /// </summary>
            public static Vector256<double> Reduce(Vector256<double> value, [ConstantExpected] byte control) { throw new PlatformNotSupportedException(); }
        }

        /// <summary>Provides access to the x86 AVX512DQ hardware instructions, that are only available to 64-bit processes, via intrinsics.</summary>
        public new abstract class X64 : Avx512F.X64
        {
            internal X64() { }

            /// <summary>Gets a value that indicates whether the APIs in this class are supported.</summary>
            /// <value><see langword="true" /> if the APIs are supported; otherwise, <see langword="false" />.</value>
            /// <remarks>A value of <see langword="false" /> indicates that the APIs will throw <see cref="PlatformNotSupportedException" />.</remarks>
            public static new bool IsSupported { [Intrinsic] get { return false; } }
        }

        /// <summary>
        ///   <para>__m512 _mm512_and_ps (__m512 a, __m512 b)</para>
        ///   <para>  VANDPS zmm1 {k1}{z}, zmm2, zmm3/m512/m32bcst</para>
        /// </summary>
        public static Vector512<float> And(Vector512<float> left, Vector512<float> right) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512d _mm512_and_pd (__m512d a, __m512d b)</para>
        ///   <para>  VANDPD zmm1 {k1}{z}, zmm2, zmm3/m512/m64bcst</para>
        /// </summary>
        public static Vector512<double> And(Vector512<double> left, Vector512<double> right) { throw new PlatformNotSupportedException(); }

        /// <summary>
        ///   <para>__m512 _mm512_andnot_ps (__m512 a, __m512 b)</para>
        ///   <para>  VANDNPS zmm1 {k1}{z}, zmm2, zmm3/m512/m32bcst</para>
        /// </summary>
        public static Vector512<float> AndNot(Vector512<float> left, Vector512<float> right) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512d _mm512_andnot_pd (__m512d a, __m512d b)</para>
        ///   <para>  VANDNPD zmm1 {k1}{z}, zmm2, zmm3/m512/m64bcst</para>
        /// </summary>
        public static Vector512<double> AndNot(Vector512<double> left, Vector512<double> right) { throw new PlatformNotSupportedException(); }

        /// <summary>
        ///   <para>__m512i _mm512_broadcast_i32x2 (__m128i a)</para>
        ///   <para>  VBROADCASTI32x2 zmm1 {k1}{z}, xmm2/m64</para>
        /// </summary>
        public static Vector512<int> BroadcastPairScalarToVector512(Vector128<int> value) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512i _mm512_broadcast_i32x2 (__m128i a)</para>
        ///   <para>  VBROADCASTI32x2 zmm1 {k1}{z}, xmm2/m64</para>
        /// </summary>
        public static Vector512<uint> BroadcastPairScalarToVector512(Vector128<uint> value) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512 _mm512_broadcast_f32x2 (__m128 a)</para>
        ///   <para>  VBROADCASTF32x2 zmm1 {k1}{z}, xmm2/m64</para>
        /// </summary>
        public static Vector512<float> BroadcastPairScalarToVector512(Vector128<float> value) { throw new PlatformNotSupportedException(); }

        /// <summary>
        ///   <para>__m512i _mm512_broadcast_i64x2 (__m128i const * mem_addr)</para>
        ///   <para>  VBROADCASTI64x2 zmm1 {k1}{z}, m128</para>
        /// </summary>
        public static unsafe Vector512<long> BroadcastVector128ToVector512(long* address) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512i _mm512_broadcast_i64x2 (__m128i const * mem_addr)</para>
        ///   <para>  VBROADCASTI64x2 zmm1 {k1}{z}, m128</para>
        /// </summary>
        public static unsafe Vector512<ulong> BroadcastVector128ToVector512(ulong* address) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512d _mm512_broadcast_f64x2 (__m128d const * mem_addr)</para>
        ///   <para>  VBROADCASTF64x2 zmm1 {k1}{z}, m128</para>
        /// </summary>
        public static unsafe Vector512<double> BroadcastVector128ToVector512(double* address) { throw new PlatformNotSupportedException(); }

        /// <summary>
        ///   <para>__m512i _mm512_broadcast_i32x8 (__m256i const * mem_addr)</para>
        ///   <para>  VBROADCASTI32x8 zmm1 {k1}{z}, m256</para>
        /// </summary>
        public static unsafe Vector512<int> BroadcastVector256ToVector512(int* address) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512i _mm512_broadcast_i32x8 (__m256i const * mem_addr)</para>
        ///   <para>  VBROADCASTI32x8 zmm1 {k1}{z}, m256</para>
        /// </summary>
        public static unsafe Vector512<uint> BroadcastVector256ToVector512(uint* address) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512 _mm512_broadcast_f32x8 (__m256 const * mem_addr)</para>
        ///   <para>  VBROADCASTF32x8 zmm1 {k1}{z}, m256</para>
        /// </summary>
        public static unsafe Vector512<float> BroadcastVector256ToVector512(float* address) { throw new PlatformNotSupportedException(); }

        /// <summary>
        ///   <para>__mmask8 _mm512_fpclass_pd_mask (__m512d a, int c)</para>
        ///   <para>  VFPCLASSPD k2 {k1}, zmm2/m512/m64bcst, imm8</para>
        /// </summary>
        public static Vector512<double> Classify(Vector512<double> value, [ConstantExpected] byte control) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__mmask16 _mm512_fpclass_ps_mask (__m512 a, int c)</para>
        ///   <para>  VFPCLASSPS k2 {k1}, zmm2/m512/m32bcst, imm8</para>
        /// </summary>
        public static Vector512<float> Classify(Vector512<float> value, [ConstantExpected] byte control) { throw new PlatformNotSupportedException(); }

        /// <summary>
        ///   <para>__mmask8 _mm_fpclass_sd_mask (__m128d a, int c)</para>
        ///   <para>  VFPCLASSSS k2 {k1}, xmm2/m32, imm8</para>
        /// </summary>
        public static Vector128<double> ClassifyScalar(Vector128<double> value, [ConstantExpected] byte control) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__mmask8 _mm_fpclass_ss_mask (__m128 a, int c)</para>
        ///   <para>  VFPCLASSSS k2 {k1}, xmm2/m32, imm8</para>
        /// </summary>
        public static Vector128<float> ClassifyScalar(Vector128<float> value, [ConstantExpected] byte control) { throw new PlatformNotSupportedException(); }

        /// <summary>
        ///   <para>__m512 _mm512_cvtepi64_ps (__m512i a)</para>
        ///   <para>  VCVTQQ2PS ymm1 {k1}{z}, zmm2/m512/m64bcst</para>
        /// </summary>
        public static Vector256<float> ConvertToVector256Single(Vector512<long> value) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512 _mm512_cvtepu64_ps (__m512i a)</para>
        ///   <para>  VCVTUQQ2PS ymm1 {k1}{z}, zmm2/m512/m64bcst</para>
        /// </summary>
        public static Vector256<float> ConvertToVector256Single(Vector512<ulong> value) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m256 _mm512_cvt_roundepi64_ps (__m512i a, int r)</para>
        ///   <para>  VCVTQQ2PS ymm1, zmm2 {er}</para>
        /// </summary>
        public static Vector256<float> ConvertToVector256Single(Vector512<long> value, [ConstantExpected(Max = FloatRoundingMode.ToZero)] FloatRoundingMode mode) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m256 _mm512_cvt_roundepu64_ps (__m512i a, int r)</para>
        ///   <para>  VCVTUQQ2PS ymm1, zmm2 {er}</para>
        /// </summary>
        public static Vector256<float> ConvertToVector256Single(Vector512<ulong> value, [ConstantExpected(Max = FloatRoundingMode.ToZero)] FloatRoundingMode mode) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512d _mm512_cvtepi64_pd (__m512i a)</para>
        ///   <para>  VCVTQQ2PD zmm1 {k1}{z}, zmm2/m512/m64bcst</para>
        /// </summary>
        public static Vector512<double> ConvertToVector512Double(Vector512<long> value) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512d _mm512_cvtepu64_pd (__m512i a)</para>
        ///   <para>  VCVTUQQ2PD zmm1 {k1}{z}, zmm2/m512/m64bcst</para>
        /// </summary>
        public static Vector512<double> ConvertToVector512Double(Vector512<ulong> value) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512d _mm512_cvt_roundepi64_pd (__m512i a, int r)</para>
        ///   <para>  VCVTQQ2PD zmm1, zmm2 {er}</para>
        /// </summary>
        public static Vector512<double> ConvertToVector512Double(Vector512<long> value, [ConstantExpected(Max = FloatRoundingMode.ToZero)] FloatRoundingMode mode) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512d _mm512_cvt_roundepu64_pd (__m512i a, int r)</para>
        ///   <para>  VCVTUQQ2PD zmm1, zmm2 {er}</para>
        /// </summary>
        public static Vector512<double> ConvertToVector512Double(Vector512<ulong> value, [ConstantExpected(Max = FloatRoundingMode.ToZero)] FloatRoundingMode mode) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512i _mm512_cvtps_epi64 (__m512 a)</para>
        ///   <para>  VCVTPS2QQ zmm1 {k1}{z}, ymm2/m256/m32bcst{er}</para>
        /// </summary>
        public static Vector512<long> ConvertToVector512Int64(Vector256<float> value) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512i _mm512_cvtpd_epi64 (__m512d a)</para>
        ///   <para>  VCVTPD2QQ zmm1 {k1}{z}, zmm2/m512/m64bcst{er}</para>
        /// </summary>
        public static Vector512<long> ConvertToVector512Int64(Vector512<double> value) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512i _mm512_cvt_roundps_epi64 (__m512 a, int r)</para>
        ///   <para>  VCVTPS2QQ zmm1, ymm2 {er}</para>
        /// </summary>
        public static Vector512<long> ConvertToVector512Int64(Vector256<float> value, [ConstantExpected(Max = FloatRoundingMode.ToZero)] FloatRoundingMode mode) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512i _mm512_cvt_roundpd_epi64 (__m512d a, int r)</para>
        ///   <para>  VCVTPD2QQ zmm1, zmm2 {er}</para>
        /// </summary>
        public static Vector512<long> ConvertToVector512Int64(Vector512<double> value, [ConstantExpected(Max = FloatRoundingMode.ToZero)] FloatRoundingMode mode) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512i _mm512_cvttps_epi64 (__m512 a)</para>
        ///   <para>  VCVTTPS2QQ zmm1 {k1}{z}, ymm2/m256/m32bcst{er}</para>
        /// </summary>
        public static Vector512<long> ConvertToVector512Int64WithTruncation(Vector256<float> value) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512i _mm512_cvttpd_epi64 (__m512 a)</para>
        ///   <para>  VCVTTPD2QQ zmm1 {k1}{z}, zmm2/m512/m64bcst{sae}</para>
        /// </summary>
        public static Vector512<long> ConvertToVector512Int64WithTruncation(Vector512<double> value) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512i _mm512_cvtps_epu64 (__m512 a)</para>
        ///   <para>  VCVTPS2UQQ zmm1 {k1}{z}, ymm2/m256/m32bcst{er}</para>
        /// </summary>
        public static Vector512<ulong> ConvertToVector512UInt64(Vector256<float> value) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512i _mm512_cvtpd_epu64 (__m512d a)</para>
        ///   <para>  VCVTPD2UQQ zmm1 {k1}{z}, zmm2/m512/m64bcst{er}</para>
        /// </summary>
        public static Vector512<ulong> ConvertToVector512UInt64(Vector512<double> value) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512i _mm512_cvt_roundps_epu64 (__m512 a, int r)</para>
        ///   <para>  VCVTPS2UQQ zmm1 {k1}{z}, ymm2/m256/m32bcst{er}</para>
        /// </summary>
        public static Vector512<ulong> ConvertToVector512UInt64(Vector256<float> value, [ConstantExpected(Max = FloatRoundingMode.ToZero)] FloatRoundingMode mode) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512i _mm512_cvt_roundpd_epu64 (__m512d a, int r)</para>
        ///   <para>  VCVTPD2UQQ zmm1 {k1}{z}, zmm2/m512/m64bcst{er}</para>
        /// </summary>
        public static Vector512<ulong> ConvertToVector512UInt64(Vector512<double> value, [ConstantExpected(Max = FloatRoundingMode.ToZero)] FloatRoundingMode mode) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512i _mm512_cvttps_epu64 (__m512 a)</para>
        ///   <para>  VCVTTPS2UQQ zmm1 {k1}{z}, ymm2/m256/m32bcst{er}</para>
        /// </summary>
        public static Vector512<ulong> ConvertToVector512UInt64WithTruncation(Vector256<float> value) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512i _mm512_cvttpd_epu64 (__m512d a)</para>
        ///   <para>  VCVTTPD2UQQ zmm1 {k1}{z}, zmm2/m512/m64bcst{er}</para>
        /// </summary>
        public static Vector512<ulong> ConvertToVector512UInt64WithTruncation(Vector512<double> value) { throw new PlatformNotSupportedException(); }

        /// <summary>
        ///   <para>__m128i _mm512_extracti64x2_epi64 (__m512i a, const int imm8)</para>
        ///   <para>  VEXTRACTI64x2 xmm1/m128 {k1}{z}, zmm2, imm8</para>
        /// </summary>
        public static new Vector128<long> ExtractVector128(Vector512<long> value, [ConstantExpected] byte index) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m128i _mm512_extracti64x2_epi64 (__m512i a, const int imm8)</para>
        ///   <para>  VEXTRACTI64x2 xmm1/m128 {k1}{z}, zmm2, imm8</para>
        /// </summary>
        public static new Vector128<ulong> ExtractVector128(Vector512<ulong> value, [ConstantExpected] byte index) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m128d _mm512_extractf64x2_pd (__m512d a, const int imm8)</para>
        ///   <para>  VEXTRACTF64x2 xmm1/m128 {k1}{z}, zmm2, imm8</para>
        /// </summary>
        public static new Vector128<double> ExtractVector128(Vector512<double> value, [ConstantExpected] byte index) { throw new PlatformNotSupportedException(); }

        /// <summary>
        ///   <para>__m256i _mm512_extracti32x8_epi32 (__m512i a, const int imm8)</para>
        ///   <para>  VEXTRACTI32x8 ymm1/m256 {k1}{z}, zmm2, imm8</para>
        /// </summary>
        public static new Vector256<int> ExtractVector256(Vector512<int> value, [ConstantExpected] byte index) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m256i _mm512_extracti32x8_epi32 (__m512i a, const int imm8)</para>
        ///   <para>  VEXTRACTI32x8 ymm1/m256 {k1}{z}, zmm2, imm8</para>
        /// </summary>
        public static new Vector256<uint> ExtractVector256(Vector512<uint> value, [ConstantExpected] byte index) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m256 _mm512_extractf32x8_ps (__m512 a, const int imm8)</para>
        ///   <para>  VEXTRACTF32x8 ymm1/m256 {k1}{z}, zmm2, imm8</para>
        /// </summary>
        public static new Vector256<float> ExtractVector256(Vector512<float> value, [ConstantExpected] byte index) { throw new PlatformNotSupportedException(); }

        /// <summary>
        ///   <para>__m512i _mm512_inserti64x2_si512 (__m512i a, __m128i b, const int imm8)</para>
        ///   <para>  VINSERTI64x2 zmm1 {k1}{z}, zmm2, xmm3/m128, imm8</para>
        /// </summary>
        public static new Vector512<long> InsertVector128(Vector512<long> value, Vector128<long> data, [ConstantExpected] byte index) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512i _mm512_inserti64x2_si512 (__m512i a, __m128i b, const int imm8)</para>
        ///   <para>  VINSERTI64x2 zmm1 {k1}{z}, zmm2, xmm3/m128, imm8</para>
        /// </summary>
        public static new Vector512<ulong> InsertVector128(Vector512<ulong> value, Vector128<ulong> data, [ConstantExpected] byte index) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512d _mm512_insertf64x2_pd (__m512d a, __m128d b, int imm8)</para>
        ///   <para>  VINSERTF64x2 zmm1 {k1}{z}, zmm2, xmm3/m128, imm8</para>
        /// </summary>
        public static new Vector512<double> InsertVector128(Vector512<double> value, Vector128<double> data, [ConstantExpected] byte index) { throw new PlatformNotSupportedException(); }

        /// <summary>
        ///   <para>__m512i _mm512_inserti32x8_si512 (__m512i a, __m256i b, const int imm8)</para>
        ///   <para>  VINSERTI32x8 zmm1 {k1}{z}, zmm2, xmm3/m256, imm8</para>
        /// </summary>
        public static new Vector512<int> InsertVector256(Vector512<int> value, Vector256<int> data, [ConstantExpected] byte index) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512i _mm512_inserti32x8_si512 (__m512i a, __m256i b, const int imm8)</para>
        ///   <para>  VINSERTI32x8 zmm1 {k1}{z}, zmm2, xmm3/m256, imm8</para>
        /// </summary>
        public static new Vector512<uint> InsertVector256(Vector512<uint> value, Vector256<uint> data, [ConstantExpected] byte index) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512 _mm512_insertf32x8_ps (__m512 a, __m256 b, int imm8)</para>
        ///   <para>  VINSERTF32x8 zmm1 {k1}{z}, zmm2, xmm3/m256, imm8</para>
        /// </summary>
        public static new Vector512<float> InsertVector256(Vector512<float> value, Vector256<float> data, [ConstantExpected] byte index) { throw new PlatformNotSupportedException(); }

        /// <summary>
        ///   <para>unsigned int _cvtmask8_u32 (__mmask8 a)</para>
        ///   <para>  KMOVB r32, k1</para>
        /// </summary>
        public static new int MoveMask(Vector128<double> value) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>unsigned int _cvtmask8_u32 (__mmask8 a)</para>
        ///   <para>  KMOVB r32, k1</para>
        /// </summary>
        public static int MoveMask(Vector128<short> value) { throw new PlatformNotSupportedException(); }

        /// <summary>
        ///   <para>unsigned int _cvtmask8_u32 (__mmask8 a)</para>
        ///   <para>  KMOVB r32, k1</para>
        /// </summary>
        public static int MoveMask(Vector128<int> value) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>unsigned int _cvtmask8_u32 (__mmask8 a)</para>
        ///   <para>  KMOVB r32, k1</para>
        /// </summary>
        public static int MoveMask(Vector128<long> value) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>unsigned int _cvtmask8_u32 (__mmask8 a)</para>
        ///   <para>  KMOVB r32, k1</para>
        /// </summary>
        public static new int MoveMask(Vector128<float> value) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>unsigned int _cvtmask8_u32 (__mmask8 a)</para>
        ///   <para>  KMOVB r32, k1</para>
        /// </summary>
        public static int MoveMask(Vector128<ushort> value) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>unsigned int _cvtmask8_u32 (__mmask8 a)</para>
        ///   <para>  KMOVB r32, k1</para>
        /// </summary>
        public static int MoveMask(Vector128<uint> value) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>unsigned int _cvtmask8_u32 (__mmask8 a)</para>
        ///   <para>  KMOVB r32, k1</para>
        /// </summary>
        public static int MoveMask(Vector128<ulong> value) { throw new PlatformNotSupportedException(); }

        /// <summary>
        ///   <para>unsigned int _cvtmask8_u32 (__mmask8 a)</para>
        ///   <para>  KMOVB r32, k1</para>
        /// </summary>
        public static new int MoveMask(Vector256<double> value) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>unsigned int _cvtmask8_u32 (__mmask8 a)</para>
        ///   <para>  KMOVB r32, k1</para>
        /// </summary>
        public static int MoveMask(Vector256<int> value) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>unsigned int _cvtmask8_u32 (__mmask8 a)</para>
        ///   <para>  KMOVB r32, k1</para>
        /// </summary>
        public static int MoveMask(Vector256<long> value) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>unsigned int _cvtmask8_u32 (__mmask8 a)</para>
        ///   <para>  KMOVB r32, k1</para>
        /// </summary>
        public static new int MoveMask(Vector256<float> value) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>unsigned int _cvtmask8_u32 (__mmask8 a)</para>
        ///   <para>  KMOVB r32, k1</para>
        /// </summary>
        public static int MoveMask(Vector256<uint> value) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>unsigned int _cvtmask8_u32 (__mmask8 a)</para>
        ///   <para>  KMOVB r32, k1</para>
        /// </summary>
        public static int MoveMask(Vector256<ulong> value) { throw new PlatformNotSupportedException(); }

        /// <summary>
        ///   <para>unsigned int _cvtmask8_u32 (__mmask8 a)</para>
        ///   <para>  KMOVB r32, k1</para>
        /// </summary>
        public static int MoveMask(Vector512<double> value) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>unsigned int _cvtmask8_u32 (__mmask8 a)</para>
        ///   <para>  KMOVB r32, k1</para>
        /// </summary>
        public static int MoveMask(Vector512<long> value) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>unsigned int _cvtmask8_u32 (__mmask8 a)</para>
        ///   <para>  KMOVB r32, k1</para>
        /// </summary>
        public static int MoveMask(Vector512<ulong> value) { throw new PlatformNotSupportedException(); }

        /// <summary>
        ///   <para>__m512i _mm512_mullo_epi64 (__m512i a, __m512i b)</para>
        ///   <para>  VPMULLQ zmm1 {k1}{z}, zmm2, zmm3/m512/m64bcst</para>
        /// </summary>
        public static Vector512<long> MultiplyLow(Vector512<long> left, Vector512<long> right) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512i _mm512_mullo_epi64 (__m512i a, __m512i b)</para>
        ///   <para>  VPMULLQ zmm1 {k1}{z}, zmm2, zmm3/m512/m64bcst</para>
        /// </summary>
        public static Vector512<ulong> MultiplyLow(Vector512<ulong> left, Vector512<ulong> right) { throw new PlatformNotSupportedException(); }

        /// <summary>
        ///   <para>__m512 _mm512_or_ps (__m512 a, __m512 b)</para>
        ///   <para>  VORPS zmm1 {k1}{z}, zmm2, zmm3/m512/m32bcst</para>
        /// </summary>
        public static Vector512<float> Or(Vector512<float> left, Vector512<float> right) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512d _mm512_or_pd (__m512d a, __m512d b)</para>
        ///   <para>  VORPD zmm1 {k1}{z}, zmm2, zmm3/m512/m64bcst</para>
        /// </summary>
        public static Vector512<double> Or(Vector512<double> left, Vector512<double> right) { throw new PlatformNotSupportedException(); }

        /// <summary>
        ///   <para>__m512 _mm512_range_ps(__m512 a, __m512 b, int imm);</para>
        ///   <para>  VRANGEPS zmm1 {k1}{z}, zmm2, zmm3/m512/m32bcst{sae}, imm8</para>
        /// </summary>
        public static Vector512<float> Range(Vector512<float> left, Vector512<float> right, [ConstantExpected(Max = (byte)(0x0F))] byte control) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512d _mm512_range_pd(__m512d a, __m512d b, int imm);</para>
        ///   <para>  VRANGEPD zmm1 {k1}{z}, zmm2, zmm3/m512/m64bcst{sae}, imm8</para>
        /// </summary>
        public static Vector512<double> Range(Vector512<double> left, Vector512<double> right, [ConstantExpected(Max = (byte)(0x0F))] byte control) { throw new PlatformNotSupportedException(); }

        /// <summary>
        ///   <para>__m128 _mm_range_ss(__m128 a, __m128 b, int imm);</para>
        ///   <para>  VRANGESS xmm1 {k1}{z}, xmm2, xmm3/m32{sae}, imm8</para>
        /// </summary>
        public static Vector128<float> RangeScalar(Vector128<float> left, Vector128<float> right, [ConstantExpected(Max = (byte)(0x0F))] byte control) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m128d _mm_range_sd(__m128d a, __m128d b, int imm);</para>
        ///   <para>  VRANGESD xmm1 {k1}{z}, xmm2, xmm3/m64{sae}, imm8</para>
        /// </summary>
        public static Vector128<double> RangeScalar(Vector128<double> left, Vector128<double> right, [ConstantExpected(Max = (byte)(0x0F))] byte control) { throw new PlatformNotSupportedException(); }

        /// <summary>
        ///   <para>__m512 _mm512_reduce_ps(__m512 a, int imm);</para>
        ///   <para>  VREDUCEPS zmm1 {k1}{z}, zmm2/m512/m32bcst{sae}, imm8</para>
        /// </summary>
        public static Vector512<float> Reduce(Vector512<float> value, [ConstantExpected] byte control) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512d _mm512_reduce_pd(__m512d a, int imm);</para>
        ///   <para>  VREDUCEPD zmm1 {k1}{z}, zmm2/m512/m64bcst{sae}, imm8</para>
        /// </summary>
        public static Vector512<double> Reduce(Vector512<double> value, [ConstantExpected] byte control) { throw new PlatformNotSupportedException(); }

        /// <summary>
        ///   <para>__m128 _mm_reduce_ss(__m128 a, int imm);</para>
        ///   <para>  VREDUCESS xmm1 {k1}{z}, xmm2, xmm3/m32{sae}, imm8</para>
        /// </summary>
        public static Vector128<float> ReduceScalar(Vector128<float> value, [ConstantExpected] byte control) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m128d _mm_reduce_sd(__m128d a, int imm);</para>
        ///   <para>  VREDUCESD xmm1 {k1}{z}, xmm2, xmm3/m64{sae}, imm8</para>
        /// </summary>
        public static Vector128<double> ReduceScalar(Vector128<double> value, [ConstantExpected] byte control) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m128 _mm_reduce_ss(__m128 a, __m128 b, int imm);</para>
        ///   <para>  VREDUCESS xmm1 {k1}{z}, xmm2, xmm3/m32{sae}, imm8</para>
        ///   <para>The above native signature does not exist. We provide this additional overload for consistency with the other scalar APIs.</para>
        /// </summary>
        public static Vector128<float> ReduceScalar(Vector128<float> upper, Vector128<float> value, [ConstantExpected] byte control) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m128d _mm_reduce_sd(__m128d a, __m128d b, int imm);</para>
        ///   <para>  VREDUCESD xmm1 {k1}{z}, xmm2, xmm3/m64{sae}, imm8</para>
        ///   <para>The above native signature does not exist. We provide this additional overload for consistency with the other scalar APIs.</para>
        /// </summary>
        public static Vector128<double> ReduceScalar(Vector128<double> upper, Vector128<double> value, [ConstantExpected] byte control) { throw new PlatformNotSupportedException(); }

        /// <summary>
        ///   <para>__m512 _mm512_xor_ps (__m512 a, __m512 b)</para>
        ///   <para>  VXORPS zmm1 {k1}{z}, zmm2, zmm3/m512/m32bcst</para>
        /// </summary>
        public static Vector512<float> Xor(Vector512<float> left, Vector512<float> right) { throw new PlatformNotSupportedException(); }
        /// <summary>
        ///   <para>__m512d _mm512_xor_pd (__m512d a, __m512d b)</para>
        ///   <para>  VXORPD zmm1 {k1}{z}, zmm2, zmm3/m512/m64bcst</para>
        /// </summary>
        public static Vector512<double> Xor(Vector512<double> left, Vector512<double> right) { throw new PlatformNotSupportedException(); }
    }
}
